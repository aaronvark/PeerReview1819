using Breakin.GameManagement;
using UnityEngine;

namespace Breakin.FSM.States
{
    public class StartLevelState : AbstractState
    {
        public override event StateChange RequestTransition;

        private LevelData loadedData;

        public override void Start()
        {
            EventManager.deactivate?.Invoke();

            // Load the data to check if it's null so a transition to level won can be activated in case it is
            EventManager.broadcastLevel += data => loadedData = data;
            // Tell listeners to set up the level
            EventManager.levelSetup?.Invoke();

            EventManager.displayMessage?.Invoke($"{loadedData.name}\nClick anywhere to start!");
        }

        public override void Run()
        {
            // Transition to game won immediately if the loaded data was null
            if (loadedData == null)
            {
                RequestTransition?.Invoke(new GameWonState());
            }

            // Wait for a mouse click to progress to the next state
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