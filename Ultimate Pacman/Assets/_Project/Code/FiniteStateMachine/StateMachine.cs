public sealed class StateMachine
{
    IState currentState = null;

    public StateMachine(IState startState)
    {
        currentState = startState;
    }

    public void Update()
    {
        if (currentState != null)
        {
            currentState.Run();
        }
    }

    public void SwitchState(IState newState)
    {
        // Complete current state
        if (currentState != null)
        {
            currentState.OnStateSwitch -= SwitchState;
            currentState.Complete();
        }

        // Initialize new state
        newState.Start();
        newState.OnStateSwitch -= SwitchState;

        // Assign the new state
        currentState = newState;
    }
}
