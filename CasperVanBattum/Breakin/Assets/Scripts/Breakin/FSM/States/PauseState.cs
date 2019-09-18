using Breakin.GameManagement;
using UnityEngine;

namespace Breakin.FSM.States
{
    public class PauseState : IState
    {
        public event StateChange RequestTransition;

        private IState originalState;

        public PauseState(IState originalState)
        {
            this.originalState = originalState;
        }

        public void Start()
        {
            EventManager.displayMessage.Invoke("Press esc to resume");
        }

        public void Run()
        {
            if (Input.GetKeyDown(KeyCode.Escape))
            {
                RequestTransition?.Invoke(originalState);
            }
        }

        public void Complete()
        {
            EventManager.hideMessage?.Invoke();
        }
    }
}