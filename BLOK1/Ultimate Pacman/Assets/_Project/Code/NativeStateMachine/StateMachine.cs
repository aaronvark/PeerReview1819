public sealed class StateMachine
{
    public IState CurrentState { get; private set; }

    public StateMachine(IState startState)
    {
        CurrentState = startState;
        CurrentState.OnStateEnter();
    }

    public void Update()
    {
        if (CurrentState != null)
        {
            CurrentState.OnStateUpdate();
        }
    }

    public void SwitchState(IState newState)
    {
        // Complete current state
        if (CurrentState != null)
        {
            CurrentState.OnStateSwitch -= SwitchState;
            CurrentState.OnStateExit();
        }

        if (newState != null)
        {
            // Initialize new state
            newState.OnStateEnter();
            newState.OnStateSwitch += SwitchState;
        }

        // Assign the new state
        CurrentState = newState;
    }
}
