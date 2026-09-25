#!/usr/bin/env python3
"""Static checks for Assets/_Project/Config/Tables/*.csv (no Unity needed).

1. Every header is a public field of the matching definition class (parsed from .cs).
2. Keys are unique; enum cells are valid enum NAMES; bool/int/float cells parse.
3. Cross-table refs: weapons.ComboSequence -> attacks.AttackId, quest prerequisites.
4. Parity: each row equals the committed .asset values. A mismatch means the CSV
   was edited but ConfigTableImporter.ImportAll was not run (or vice versa) —
   run the importer in Unity and commit the regenerated assets together.
Exit code 0 = all good.
"""
import csv, glob, io, os, re, sys

ROOT = os.path.abspath(os.path.join(os.path.dirname(__file__), '..', '..', 'Assets', '_Project'))
TABLES = os.path.join(ROOT, 'Config', 'Tables')
errors = []

def cs_fields(rel):
    src = open(os.path.join(ROOT, rel), encoding='utf-8').read()
    return dict((n, t) for t, n in re.findall(
        r'^\s*(?:\[[^\]]*\]\s*)*public\s+([\w\[\]]+)\s+(\w+)\s*(?:=[^;]*)?;', src, re.M))

def cs_enum(rel, name):
    src = open(os.path.join(ROOT, rel), encoding='utf-8').read()
    body = re.search(r'enum\s+' + name + r'\s*\{(.*?)\}', src, re.S).group(1)
    body = re.sub(r'//.*', '', body)
    return [m.split('=')[0].strip() for m in body.split(',') if m.strip()]

ENUMS = {
    'WeaponType': cs_enum('Combat/Framework/WeaponType.cs', 'WeaponType'),
    'HitShapeType': cs_enum('Combat/Framework/HitShapeType.cs', 'HitShapeType'),
    'AttackInputMode': cs_enum('Combat/Framework/AttackInputMode.cs', 'AttackInputMode'),
    'SpiritSealTriggerType': cs_enum('Combat/Framework/SpiritSeals/SpiritSealTriggerType.cs', 'SpiritSealTriggerType'),
    'QuestObjectiveType': cs_enum('Gameplay/Quests/QuestState.cs', 'QuestObjectiveType'),
    'QuestRewardType': cs_enum('Gameplay/Quests/QuestState.cs', 'QuestRewardType'),
}

def read(name):
    text = open(os.path.join(TABLES, name), encoding='utf-8-sig').read()
    rows = [r for r in csv.reader(io.StringIO(text))
            if r and any(c.strip() for c in r) and not r[0].startswith('#')]
    header, data = rows[0], rows[1:]
    return header, [dict(zip(header, [c.strip() for c in r])) for r in data]

def check_cell(table, key, col, typ, cell):
    if cell == '':
        return
    where = f'{table} [{key}] {col}'
    try:
        if typ in ENUMS:
            if cell not in ENUMS[typ]:
                errors.append(f'{where}: "{cell}" not in {typ} {ENUMS[typ]}')
        elif typ == 'int': int(cell)
        elif typ == 'float': float(cell)
        elif typ == 'bool':
            if cell.lower() not in ('true', 'false', '1', '0'): raise ValueError
        elif typ == 'Vector3':
            assert len([float(x) for x in cell.split(';')]) == 3
    except (ValueError, AssertionError):
        errors.append(f'{where}: "{cell}" is not a valid {typ}')

def check_table(name, cls_rel, key, ignored=()):
    fields = cs_fields(cls_rel)
    header, rows = read(name)
    for col in header:
        if col not in fields and col not in ignored:
            errors.append(f'{name}: column "{col}" is not a public field of {os.path.basename(cls_rel)}')
    seen = set()
    for r in rows:
        k = r.get(key, '')
        if not k: errors.append(f'{name}: row with empty {key}')
        if k in seen: errors.append(f'{name}: duplicate {key} "{k}"')
        seen.add(k)
        for col in header:
            if col in fields: check_cell(name, k, col, fields[col], r[col])
    return fields, rows

atk_fields, attacks = check_table('attacks.csv', 'Combat/Framework/AttackDefinition.cs', 'AttackId')
wpn_fields, weapons = check_table('weapons.csv', 'Combat/Framework/WeaponDefinition.cs', 'WeaponType', ('ComboSequence',))
seal_fields, seals = check_table('spirit_seals.csv', 'Combat/Framework/SpiritSeals/SpiritSealDefinition.cs', 'SealId')
q_fields, quests = check_table('quests.csv', 'Gameplay/Quests/QuestDefinition.cs', 'QuestNumId')
m_fields, monsters = check_table('monsters.csv', 'Config/Core/MonsterTableRow.cs', 'MonsterId')
for m in monsters:
    if m.get('MaxHp') and m['MaxHp'].lstrip('-').isdigit() and int(m['MaxHp']) <= 0:
        errors.append(f'monsters.csv [{m["MonsterId"]}]: MaxHp must be > 0')
monster_ids = {m['MonsterId'] for m in monsters}
for q in quests:
    if q.get('ObjectiveType') == 'KillTarget' and q.get('TargetId') not in monster_ids:
        errors.append(f'quests.csv [{q["QuestId"]}]: TargetId "{q.get("TargetId")}" is not a MonsterId in monsters.csv')

attack_ids = {a['AttackId'] for a in attacks}
for w in weapons:
    for aid in filter(None, (s.strip() for s in w['ComboSequence'].split('|'))):
        if aid not in attack_ids: errors.append(f'weapons.csv [{w["WeaponType"]}]: unknown AttackId "{aid}"')
quest_ids = {q['QuestNumId'] for q in quests}
for q in quests:
    pre = q.get('PrerequisiteQuestNumId', '0') or '0'
    if pre != '0' and pre not in quest_ids: errors.append(f'quests.csv [{q["QuestId"]}]: prerequisite {pre} missing')
if len(quests) > 8: errors.append(f'quests.csv: {len(quests)} quests > PlayerQuestLog.Capacity 8')

# ---- parity with committed assets ----
def _odd_backslashes_before(s, end):
    n = 0
    while end - n - 1 >= 0 and s[end - n - 1] == '\\': n += 1
    return n % 2 == 1

def _quoted_closed(v):
    return len(v) >= 2 and v.endswith('"') and not _odd_backslashes_before(v, len(v) - 1)

def asset_values(path):
    vals = {}
    lines = open(path, encoding='utf-8').read().split('\n')
    i = 0
    while i < len(lines):
        m = re.match(r'^  (\w+): (.+)$', lines[i].rstrip('\r'))
        i += 1
        if not m: continue
        k, v = m.groups()
        if v.startswith('"'):
            # Unity folds long double-quoted strings onto indented continuation lines.
            # YAML joins a folded line break as one space, or as nothing after a
            # line-ending escape backslash.
            while not _quoted_closed(v) and i < len(lines) and lines[i].startswith('    '):
                nxt = lines[i].strip(); i += 1
                v = v[:-1] + nxt if _odd_backslashes_before(v, len(v)) else v + ' ' + nxt
            v = v[1:-1].encode('utf-8').decode('unicode_escape')
        vals[k] = v
    return vals

def norm(typ, v):
    if typ in ENUMS: return ENUMS[typ][int(v)] if v.lstrip('-').isdigit() else v
    if typ == 'bool': return str(v.lower() in ('1', 'true')).lower()
    if typ in ('float', 'int'): return float(v)
    if typ == 'Vector3': return tuple(float(x) for x in re.findall(r'-?[\d.]+(?:e-?\d+)?', v))
    return v

def parity(table, rows, fields, key, asset_key, glob_pat):
    assets = {}
    for p in glob.glob(os.path.join(ROOT, glob_pat), recursive=True):
        v = asset_values(p)
        if asset_key in v: assets[v[asset_key]] = v
    if not assets:
        print(f'  parity {table}: no committed assets yet (generated by the importer in Unity) — skipped')
        return
    for r in rows:
        a = assets.get(r[key])
        if a is None:
            errors.append(f'parity {table} [{r[key]}]: no committed asset — run ConfigTableImporter.ImportAll')
            continue
        for col, typ in fields.items():
            if col in r and r[col] != '' and col in a and norm(typ, r[col]) != norm(typ, a[col]):
                errors.append(f'parity {table} [{r[key]}] {col}: csv={r[col]} asset={a[col]}')

parity('attacks.csv', attacks, atk_fields, 'AttackId', 'AttackId', 'Combat/Weapons/**/*.asset')
parity('spirit_seals.csv', seals, seal_fields, 'SealId', 'SealId', 'Combat/SpiritSeals/*.asset')
parity('quests.csv', quests, q_fields, 'QuestNumId', 'QuestNumId', 'Gameplay/Quests/Data/*.asset')

if errors:
    print('\n'.join('ERROR: ' + e for e in errors))
    print(f'validate_tables: {len(errors)} error(s)')
    sys.exit(1)
print(f'validate_tables: OK ({len(attacks)} attacks, {len(weapons)} weapons, {len(seals)} seals, {len(quests)} quests, {len(monsters)} monsters)')
