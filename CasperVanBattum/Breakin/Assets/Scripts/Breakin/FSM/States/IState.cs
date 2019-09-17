namespace Breakin.FSM.States
{
    public delegate void StateChange(IState newState);
    
    public interface IState
    {
        event StateChange RequestTransition;
        
        void Start();
        void Run();
        void Complete();
    }
}