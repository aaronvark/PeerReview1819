public interface IState
{
    IStateDelegate OnStateSwitch { get; set; }

    void Start();
    void Run();
    void Complete();
}
