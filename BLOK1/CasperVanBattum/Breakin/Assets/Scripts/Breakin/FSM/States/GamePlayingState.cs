using Breakin.GameManagement;
using UnityEngine;

namespace Breakin.FSM.States
{
    public class GamePlayingState : AbstractState
    {
        public override event StateChange RequestTransition;

        private bool initialized;

        public override void Start()
        {
            if (!initialized)
            {
                EventManager.activate?.Invoke();
                initialized = true;
            }

            // Subscribe to game loop events
            EventManager.spawnerExhausted += LoadNewLevel;
            EventManager.maxRingsReached += GameOver;
            EventManager.livesUp += GameOver;
        }

        public override void Run()
        {
            if (Input.GetKeyDown(KeyCode.Escape))
            {
                RequestTransition?.Invoke(new PauseState(this));
                return;
            }

            // Update all gameplay behaviours
            EventManager.gameUpdate?.Invoke();
        }

        public override void Complete()
        {
            // Unsubscribe from game loop events
            EventManager.spawnerExhausted -= LoadNewLevel;
            EventManager.maxRingsReached -= GameOver;
            EventManager.livesUp -= GameOver;
        }

        private void LoadNewLevel()
        {
            RequestTransition?.Invoke(new StartLevelState());
        }

        private void GameOver(string reason)
        {
            RequestTransition?.Invoke(new GameOverState(reason));
        }
    }
}