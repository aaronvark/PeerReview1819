using Breakin.GameManagement;
using Breakin.UI;
using UnityEngine;

namespace Breakin.FSM.States
{
    public class StartLevelState : AbstractState
    {
        private const string _START_MESSAGE = "Click anywhere to start!";
        
        public override event StateChange RequestTransition;

        public StartLevelState(GameManager owner) : base(owner) { }

        public override void Start()
        {
            owner.LoadNextLevel();
            
            MessageManager.Instance.ShowMessage(_START_MESSAGE);
        }

        public override void Run()
        {
            if (Input.GetMouseButtonDown(0))
            {
                RequestTransition?.Invoke(new GamePlayingState(owner));
            }
        }

        public override void Complete()
        {
            MessageManager.Instance.HideMessage();
        }
    }
}