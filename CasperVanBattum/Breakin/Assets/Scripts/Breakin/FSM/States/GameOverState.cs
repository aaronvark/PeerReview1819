using Breakin.GameManagement;
using UnityEngine;

namespace Breakin.FSM.States
{
    public class GameOverState : AbstractState
    {
        public override event StateChange RequestTransition;

        private string reason;
        
        public GameOverState(string reason)
        {
            this.reason = reason;
        }

        public override void Start()
        {
            EventManager.deactivate?.Invoke();
            EventManager.displayMessage?.Invoke(reason + "\nPress any key to start new game...");
        }

        public override void Run()
        {
            if (Input.anyKeyDown)
            {
                EventManager.reset?.Invoke();
                RequestTransition?.Invoke(new StartLevelState());
            }
        }

        public override void Complete()
        {
            EventManager.hideMessage?.Invoke();
        }
    }
}