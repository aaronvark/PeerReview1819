namespace Breakin.FSM.States
{
    public delegate bool Condition();
    
    public struct Transition
    {
        public IState From { get; }
        public IState To { get; }

        public Condition Condition { get; }

        public Transition(IState from, IState to, Condition condition)
        {
            From = from;
            To = to;
            Condition = condition;
        }
    }
}