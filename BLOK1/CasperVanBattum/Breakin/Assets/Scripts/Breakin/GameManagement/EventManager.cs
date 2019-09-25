namespace Breakin.GameManagement
{
    public delegate void MessageSender(string msg);

    public delegate void GameOverDelegate(string reason);

    public delegate void LevelBroadcaster(LevelData data);

    public static class EventManager
    {
        /// <summary>
        /// Can be invoked to display a message on the screen.
        /// </summary>
        public static MessageSender displayMessage;

        /// <summary>
        /// Can be invoked to hide a message on the screen.
        /// </summary>
        public static System.Action hideMessage;

        /// <summary>
        /// Is invoked to tell the relevant behaviours to set themselves up to play a new level.
        /// </summary>
        public static System.Action levelSetup;

        /// <summary>
        /// Is invoked to let everyone load the new level.
        /// </summary>
        public static LevelBroadcaster broadcastLevel;

        /// <summary>
        /// Is invoked to tell the relevant behaviours to start themselves.
        /// </summary>
        public static System.Action activate;

        /// <summary>
        /// Is invoked each frame to update the relevant behaviours.
        /// </summary>
        public static System.Action gameUpdate;

        /// <summary>
        /// Is invoked when the level is deactivated.
        /// </summary>
        public static System.Action deactivate;

        /// <summary>
        /// Is invoked when the game wants to start over completely.
        /// </summary>
        public static System.Action reset;

        /// <summary>
        /// Invoked when all rings have been spawned.
        /// </summary>
        public static System.Action spawnerExhausted;

        /// <summary>
        /// Invoked when the spawner wanted to spawn a ring but couldn't because the screen was full (game over
        /// condition).
        /// </summary>
        public static GameOverDelegate maxRingsReached;

        /// <summary>
        /// Invoked when the player's lives are up.
        /// </summary>
        public static GameOverDelegate livesUp;
    }
}