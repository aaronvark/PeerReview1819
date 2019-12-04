using UnityEngine;

public class FakeOut: GhostStateMachineBehaviour
{
    private const float minDistanceFromEdge = .5f;
    private const float distanceForNewRandomPoint = .1f;

    [SerializeField]
    private float distanceToFakeOut = 4f;

    private Vector2 randomPoint;

    // Distance-checking optimizations
    private float sqrDistanceToFakeOut;
    private float sqrDistanceForNewRandomPoint;

    public override void OnStateEnter(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
        base.OnStateEnter(animator, stateInfo, layerIndex);

        SetRandomPoint();
        sqrDistanceToFakeOut = distanceToFakeOut * distanceToFakeOut;
        sqrDistanceForNewRandomPoint = distanceForNewRandomPoint * distanceForNewRandomPoint;
    }

    public override void OnStateUpdate(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
        // If the distance from the player is within FakeOut range, fly towards a random point on the map.
        Vector2 distance = (Vector2)player.position - (Vector2)transform.position;
        if (distance.sqrMagnitude <= sqrDistanceToFakeOut)
        {
            distance = randomPoint - (Vector2)transform.position;

            // If the ghost is approximately at the random point, find a new random point.
            // Repeat until a random point is found the ghost isn't approximately already at.
            while (distance.sqrMagnitude <= sqrDistanceForNewRandomPoint)
            {
                SetRandomPoint();
                distance = randomPoint - (Vector2)transform.position;
            }
        }

        movement.Move(distance);
    }

    // Get a random point within the Playing Field's collider
    private void SetRandomPoint()
    {
        //Bounds bounds = GameManager.Instance.Collider.bounds;
        if(Toolbox.Instance.TryGetValue<GameManager>(out GameManager gameManager))
        {
            Bounds bounds = gameManager.Collider.bounds;

            randomPoint = new Vector2(
                Random.Range(bounds.min.x + minDistanceFromEdge, bounds.max.x - minDistanceFromEdge),
                Random.Range(bounds.min.y + minDistanceFromEdge, bounds.max.y - minDistanceFromEdge)
            );
        }
    }
}
