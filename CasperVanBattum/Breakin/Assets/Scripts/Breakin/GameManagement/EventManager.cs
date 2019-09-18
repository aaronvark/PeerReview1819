namespace Breakin.GameManagement
{
    public delegate void MessageSender(string msg);
    
    public static class EventManager
    {
        public static MessageSender displayMessage;
        public static System.Action hideMessage;

        public static System.Action levelSetup;
        public static System.Action activate;
        public static System.Action gameUpdate;
        public static System.Action pauseUpdate;

    }
}