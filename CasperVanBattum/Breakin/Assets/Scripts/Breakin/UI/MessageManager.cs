namespace Breakin.UI
{
    public delegate void MessageSender(string msg);

    public interface IMessenger
    {
        event MessageSender MessageSent;
        event System.Action MessageHidden;

        void ShowMessage(string msg);
        void HideMessage();
    }

    public class MessageManager : IMessenger
    {
        private static IMessenger instance;
        public static IMessenger Instance => instance ?? (instance = new MessageManager());

        public event MessageSender MessageSent;
        public event System.Action MessageHidden;

        public void ShowMessage(string msg)
        {
            MessageSent?.Invoke(msg);
        }

        public void HideMessage()
        {
            MessageHidden?.Invoke();
        }
    }
}