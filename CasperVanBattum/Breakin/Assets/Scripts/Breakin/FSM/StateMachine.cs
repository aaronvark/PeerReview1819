using Breakin.FSM.States;
using UnityEngine;

namespace Breakin.FSM
{
    public class StateMachine
    {
        private IState currentState;

        public StateMachine(IState initState)
        {
            SwitchState(initState);
        }

        public void Update()
        {
            currentState?.Run();
        }

        public void SwitchState(IState newState)
        {
            if (currentState != null)
            {
                currentState.Complete();
                currentState.RequestTransition -= SwitchState;
            }

            LogStateChange(newState);

            newState.Start();
            newState.RequestTransition += SwitchState;
            currentState = newState;
        }

        private void LogStateChange(IState state)
        {
            Debug.Log("Entering state of type " + state.GetType().Name);
        }
    }
}