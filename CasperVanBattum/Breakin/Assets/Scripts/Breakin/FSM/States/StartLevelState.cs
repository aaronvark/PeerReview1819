using Breakin.GameManagement;
using UnityEngine;

namespace Breakin.FSM.States
{
    public class StartLevelState : AbstractState
    {
        public override event StateChange RequestTransition;
        
        private readonly LevelData data;
        
        public StartLevelState(LevelData data)
        {
            this.data = data;
        }

        public override void Start()
        {
            Debug.Log("Im starting now and im gonna create " + data.RingCount + " rings");
        }

        public override void Run()
        {
            if (Input.GetMouseButtonDown(0))
            {
                RequestTransition?.Invoke(new GamePlayingState(data));
            }
        }
    }
}