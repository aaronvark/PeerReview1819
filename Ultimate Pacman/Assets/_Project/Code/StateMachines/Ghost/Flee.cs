using UnityEngine;

public class Flee : StateMachineBehaviour
{
    [SerializeField]
    private float speedMultiplier = .5f;

    private Transform transform = null;
    private Transform player = null;
    private Movement2D movement = null;

    // OnStateEnter is called when a transition starts and the state machine starts to evaluate this state
    override public void OnStateEnter(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
        transform = animator.transform;
        player = Player.Instance.transform;
        movement = animator.GetComponent<Movement2D>();

        movement.moveSpeed *= speedMultiplier;
    }

    // OnStateUpdate is called on each Update frame between OnStateEnter and OnStateExit callbacks
    override public void OnStateUpdate(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
        Vector2 distance = transform.position - player.position;
        Vector2 direction = distance.normalized;

        movement.Move(direction);
    }

    public override void OnStateExit(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
        movement.moveSpeed /= speedMultiplier;
    }
}
