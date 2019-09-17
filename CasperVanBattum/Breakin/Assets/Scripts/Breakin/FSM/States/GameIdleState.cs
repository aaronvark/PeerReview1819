using Breakin.GameManagement;

namespace Breakin.FSM.States
{
    public class GameIdleState : AbstractState
    {
        public override event StateChange RequestTransition;
        
        public GameIdleState(GameManager owner) : base(owner) { }
    }
}