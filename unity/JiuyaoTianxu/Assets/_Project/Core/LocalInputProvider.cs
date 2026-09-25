namespace JiuyaoTianxu.Core
{
    /// <summary>
    /// The human-player input source: keyboard/mouse merged with on-screen touch
    /// controls. NetworkGameLauncher asks this (or AutoTestInputProvider under
    /// -autotest) — nothing downstream knows which device produced the input.
    /// </summary>
    public static class LocalInputProvider
    {
        public static PlayerInputData Poll()
        {
            var data = KeyboardInputProvider.Poll();
            TouchInputState.ApplyTo(ref data);
            return data;
        }

        public static bool QuestAcceptPressed() =>
            KeyboardInputProvider.QuestAcceptPressed() | TouchInputState.ConsumeQuestAccept();
    }
}
