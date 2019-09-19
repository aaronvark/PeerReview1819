using UnityEngine;

public abstract class AbstractState : IState
{
    public Transform transform;

    public AbstractState(Transform transform)
    {
        this.transform = transform;
    }

    public virtual IStateDelegate OnStateSwitch { get; set; }

    public virtual void OnStateEnter() { }
    public virtual void OnStateUpdate() { }
    public virtual void OnStateExit() { }
}
