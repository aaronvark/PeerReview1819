using Breakin.GameManagement;
using UnityEngine;

namespace Breakin.FSM.States
{
    public class PauseState : IState
    {
        public event StateChange RequestTransition;

        private readonly IState originalState;
        private float originalTimescale;

        public PauseState(IState originalState)
        {
            this.originalState = originalState;
        }

        public void Start()
        {
            EventManager.displayMessage.Invoke("Press esc to resume");

            originalTimescale = Time.timeScale;
            Time.timeScale = 0;
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
            Time.timeScale = originalTimescale;

            EventManager.hideMessage?.Invoke();
        }
    }
}