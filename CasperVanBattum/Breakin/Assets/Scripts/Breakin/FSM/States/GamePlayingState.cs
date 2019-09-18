using Breakin.GameManagement;
using UnityEngine;

namespace Breakin.FSM.States
{
    public class GamePlayingState : AbstractState
    {
        public override event StateChange RequestTransition;

        public override void Run()
        {
            if (Input.GetKeyDown(KeyCode.Escape))
            {
                RequestTransition?.Invoke(new PauseState(this));
                return;
            }
            
            // Update the game playing
            EventManager.gameUpdate?.Invoke();
        }
    }
}