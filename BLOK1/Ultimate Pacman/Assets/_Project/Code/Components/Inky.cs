public class Inky : Ghost
{
    private void Start()
    {
        defaultState = new FlankingState(transform);
        stateMachine = new StateMachine(defaultState);
    }
}
