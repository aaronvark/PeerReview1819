using UnityEngine;

public class AbstractState : IState
{
    public Transform transform;

    public AbstractState(Transform transform)
    {
        this.transform = transform;
    }

    public virtual IStateDelegate OnStateSwitch { get; set; }

    public virtual void OnStateExit() { }
    public virtual void OnStateUpdate() { }
    public virtual void OnStateEnter() { }
}
