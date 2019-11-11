using UnityEngine;

public class MoveToClosestEdgePoint : GhostStateMachineBehaviour
{
    [SerializeField]
    private float distanceFromEdge = 1f;
    [SerializeField]
    private float distanceFromPoint = 0.1f;

    private Vector2 point;

    // Distance-checking optimizations
    private float sqrDistanceFromPoint;

    public override void OnStateEnter(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
        base.OnStateEnter(animator, stateInfo, layerIndex);

        // Disable all colliders on this gameobject
        Collider2D[] playerColliders = animator.GetComponentsInChildren<Collider2D>();
        for (int i = 0; i < playerColliders.Length; i++)
            playerColliders[i].enabled = false;

        // Find the closest point 1 unit outside of the bounds of the collider and set it as the regenerationPoint
        if(Toolbox.Instance.TryGetValue<GameManager>(out GameManager gameManager))
        {
            Vector2 currentPosition = transform.position;
            //Collider2D border = GameManager.Instance.Collider;
            Collider2D border = gameManager.Collider;
            Vector2 closestPoint = border.ClosestPoint(currentPosition);
            Vector2 pointDirection = (closestPoint - currentPosition).normalized;
            point = closestPoint + pointDirection * distanceFromEdge;
        }

        sqrDistanceFromPoint = distanceFromPoint * distanceFromPoint;
    }

    public override void OnStateUpdate(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
        Vector2 distance = point - (Vector2)transform.position;

        // Check if the regenerationPoint is reached (and Regenerate if it is)
        if (distance.sqrMagnitude <= sqrDistanceFromPoint)
        {
            transform.position = point;
            animator.SetTrigger("Reached Edge Point");
            return;
        }

        movement.Move(distance);
    }
}
