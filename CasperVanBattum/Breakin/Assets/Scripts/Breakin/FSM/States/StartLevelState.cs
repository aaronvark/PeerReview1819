using Breakin.GameManagement;
using UnityEngine;

namespace Breakin.FSM.States
{
    public class StartLevelState : AbstractState
    {
        private const string _START_MESSAGE = "Click anywhere to start!";

        public override event StateChange RequestTransition;

        public override void Start()
        {
            EventManager.levelSetup?.Invoke();

            EventManager.displayMessage?.Invoke(_START_MESSAGE);
        }

        public override void Run()
        {
            if (Input.GetMouseButtonDown(0))
            {
                RequestTransition?.Invoke(new GamePlayingState());
            }
        }

        public override void Complete()
        {
            EventManager.hideMessage.Invoke();
        }
    }
}