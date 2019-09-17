using Breakin.GameManagement;
using Breakin.UI;
using UnityEngine;

namespace Breakin.FSM.States
{
    public class StartLevelState : AbstractState
    {
        public override event StateChange RequestTransition;

        private const string _START_MESSAGE = "Click anywhere to start!";
        private readonly LevelData data;
        
        public StartLevelState(LevelData data)
        {
            this.data = data;
        }

        public override void Start()
        {
            MessageManager.Instance.ShowMessage(_START_MESSAGE);
        }

        public override void Run()
        {
            if (Input.GetMouseButtonDown(0))
            {
                RequestTransition?.Invoke(new GamePlayingState(data));
            }
        }

        public override void Complete()
        {
            MessageManager.Instance.HideMessage();
        }
    }
}