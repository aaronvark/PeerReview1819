public class Clyde : Ghost
{
    private void Start()
    {
        defaultState = new FakeOutState(transform);
        stateMachine = new StateMachine(defaultState);
    }
}
