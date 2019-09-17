using Breakin.GameManagement;

namespace Breakin.FSM.States
{
    public abstract class AbstractState : IState
    {
        protected readonly GameManager owner;

        protected AbstractState(GameManager owner)
        {
            this.owner = owner;
        }
        
        public abstract event StateChange RequestTransition;
        
        public virtual void Start() { }

        public virtual void Run() { }

        public virtual void Complete() { }
    }
}