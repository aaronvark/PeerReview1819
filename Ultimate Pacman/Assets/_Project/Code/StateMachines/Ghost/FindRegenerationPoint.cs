using UnityEngine;

public class FindRegenerationPoint : StateMachineBehaviour
{
    [SerializeField]
    private float distanceFromEdge = 1f;
    [SerializeField]
    private float distanceFromPoint = 0.1f;

    private Transform transform = null;
    private Movement2D movement = null;

    private Vector2 regenerationPoint;
    private float sqrDistanceFromPoint;

    // OnStateEnter is called when a transition starts and the state machine starts to evaluate this state
    override public void OnStateEnter(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
        transform = animator.transform;
        movement = animator.GetComponent<Movement2D>();

        Collider2D[] playerColliders = animator.GetComponentsInChildren<Collider2D>();
        for (int i = 0; i < playerColliders.Length; i++)
            playerColliders[i].enabled = false;

        Vector2 currentPosition = transform.position;
        Collider2D border = GameManager.Instance.collider;
        Vector2 closestPoint = border.ClosestPoint(currentPosition);
        Vector2 pointDirection = (closestPoint - currentPosition).normalized;
        regenerationPoint = closestPoint + pointDirection * distanceFromEdge;

        sqrDistanceFromPoint = distanceFromPoint * distanceFromPoint;
    }

    // OnStateUpdate is called on each Update frame between OnStateEnter and OnStateExit callbacks
    override public void OnStateUpdate(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
        Vector2 distance = regenerationPoint - (Vector2)transform.position;

        if (distance.sqrMagnitude <= sqrDistanceFromPoint)
        {
            transform.position = regenerationPoint;
            animator.SetTrigger("Regenerate");
            return;
        }

        Vector2 direction = distance.normalized;

        movement.Move(direction);
    }
}
