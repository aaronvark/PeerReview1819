using Breakin.GameManagement;
using UnityEngine;

namespace Breakin.FSM.States
{
    public class GameWonState : AbstractState
    {
        public override void Start()
        {
            EventManager.displayMessage("You won the game! Press any key to exit");
        }

        public override void Run()
        {
            if (Input.anyKeyDown)
            {
                Application.Quit();
            }
        }
    }
}