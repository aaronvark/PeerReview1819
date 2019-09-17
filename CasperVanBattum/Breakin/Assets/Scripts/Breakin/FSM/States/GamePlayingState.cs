using Breakin.GameManagement;
using UnityEngine;

namespace Breakin.FSM.States
{
    public class GamePlayingState : AbstractState
    {
        public override event StateChange RequestTransition;
        
        private readonly LevelData data;
        
        public GamePlayingState(LevelData data)
        {
            this.data = data;
        }

        public override void Start()
        {
            Debug.Log("yo start playing bich");
        }
    }
}