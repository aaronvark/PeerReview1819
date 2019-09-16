using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Animator))]
public abstract class Ghost : MonoBehaviour, IScore
{
    public StateMachine stateMachine { get; protected set; } = null;
    public IState defaultState { get; protected set; } = null;

    [SerializeField]
    private float scoreValue = 200f;

    public Animator animator { get; protected set; }

    public float ScoreValue => scoreValue;

    public virtual void StateEnter() { }
    public virtual void StateUpdate() { }
    public virtual void StateExit() { }

    protected virtual void Awake()
    {
        animator = GetComponent<Animator>();
    }

    protected virtual void OnEnable()
    {
        GameManager.Instance.AddGhost(this);
    }

    protected virtual void OnDisable()
    {
        GameManager.Instance.RemoveGhost(this);
    }

    protected virtual void Update()
    {
        if (stateMachine != null)
            stateMachine.Update();
    }

    public void Consume()
    {
        ScoreManager.Instance.AddScore(this);
        animator.SetTrigger("Consumed");

        if (stateMachine != null)
            stateMachine.SwitchState(new FindRegenerationPointState(transform));
    }
}
