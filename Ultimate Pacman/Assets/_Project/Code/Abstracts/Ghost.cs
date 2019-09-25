using UnityEngine;

[RequireComponent(typeof(Animator))]
public abstract class Ghost : MonoBehaviour, IScore
{
    // These variables are for making the Ghost compatible with the native FiniteStateMachine.
    public StateMachine stateMachine { get; protected set; } = null;
    public IState defaultState { get; protected set; } = null;

    [SerializeField]
    private float scoreValue = 200f;

    public Animator animator { get; protected set; }

    public float ScoreValue => scoreValue;

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

        // For updating ghosts that use the native FiniteStateMachine
        if (stateMachine != null)
            stateMachine.SwitchState(new FindRegenerationPointState(transform));
    }
}
