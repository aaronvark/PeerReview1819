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

    // Distance-checking optimizations
    private float sqrDistanceFromPoint;

    override public void OnStateEnter(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
        transform = animator.transform;
        movement = animator.GetComponent<Movement2D>();

        // Disable all colliders on this gameobject
        Collider2D[] playerColliders = animator.GetComponentsInChildren<Collider2D>();
        for (int i = 0; i < playerColliders.Length; i++)
            playerColliders[i].enabled = false;

        // Find the closest point 1 unit outside of the bounds of the collider and set it as the regenerationPoint
        Vector2 currentPosition = transform.position;
        Collider2D border = GameManager.Instance.collider;
        Vector2 closestPoint = border.ClosestPoint(currentPosition);
        Vector2 pointDirection = (closestPoint - currentPosition).normalized;
        regenerationPoint = closestPoint + pointDirection * distanceFromEdge;

        sqrDistanceFromPoint = distanceFromPoint * distanceFromPoint;
    }

    override public void OnStateUpdate(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
        Vector2 distance = regenerationPoint - (Vector2)transform.position;

        // Check if the regenerationPoint is reached (and Regenerate if it is)
        if (distance.sqrMagnitude <= sqrDistanceFromPoint)
        {
            transform.position = regenerationPoint;
            animator.SetTrigger("Regenerate");
            return;
        }

        movement.Move(distance);
    }
}
