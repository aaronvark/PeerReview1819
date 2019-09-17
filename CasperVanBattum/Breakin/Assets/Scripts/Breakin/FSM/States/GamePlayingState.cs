using Breakin.GameManagement;
using UnityEngine;

namespace Breakin.FSM.States
{
    public class GamePlayingState : AbstractState
    {
        public override event StateChange RequestTransition;

        public GamePlayingState(GameManager owner) : base(owner) { }

        public override void Start()
        {
            owner.ActivateGame();
        }
    }
}