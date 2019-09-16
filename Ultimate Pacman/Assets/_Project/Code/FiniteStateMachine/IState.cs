public interface IState
{
    IStateDelegate OnStateSwitch { get; set; }

    void OnStateEnter();
    void OnStateUpdate();
    void OnStateExit();
}
