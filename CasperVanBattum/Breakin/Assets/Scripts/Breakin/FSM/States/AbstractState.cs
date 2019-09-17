namespace Breakin.FSM.States
{
    public abstract class AbstractState : IState
    {
        public abstract event StateChange RequestTransition;
        
        public virtual void Start() { }

        public virtual void Run() { }

        public virtual void Complete() { }
    }
}