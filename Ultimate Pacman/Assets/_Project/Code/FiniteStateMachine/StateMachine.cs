public sealed class StateMachine
{
    public IState currentState { get; private set; }

    public StateMachine(IState startState)
    {
        currentState = startState;
        currentState.OnStateEnter();
    }

    public void Update()
    {
        if (currentState != null)
        {
            currentState.OnStateUpdate();
        }
    }

    public void SwitchState(IState newState)
    {
        // Complete current state
        if (currentState != null)
        {
            currentState.OnStateSwitch -= SwitchState;
            currentState.OnStateExit();
        }

        if (newState != null)
        {
            // Initialize new state
            newState.OnStateEnter();
            newState.OnStateSwitch += SwitchState;
        }

        // Assign the new state
        currentState = newState;
    }
}
