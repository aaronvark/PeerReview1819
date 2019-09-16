using UnityEngine;
using System.Collections;

public class FakeOutState : GhostDefaultState
{
    private const float minDistanceFromEdge = .5f;
    private const float distanceForNewRandomPoint = .1f;

    [SerializeField]
    private float distanceToFakeOut = 4.8f;

    private Vector2 randomPoint;

    private float sqrDistanceToFakeOut;
    private float sqrDistanceForNewRandomPoint;

    public FakeOutState(Transform transform) : base(transform) { }

    public override void OnStateEnter()
    {
        base.OnStateEnter();

        SetRandomPoint();
        sqrDistanceToFakeOut = distanceToFakeOut * distanceToFakeOut;
        sqrDistanceForNewRandomPoint = distanceForNewRandomPoint * distanceForNewRandomPoint;
    }

    public override void OnStateUpdate()
    {
        Vector2 distance = (Vector2)player.position - (Vector2)transform.position;
        if (distance.sqrMagnitude <= sqrDistanceToFakeOut)
        {
            distance = randomPoint - (Vector2)transform.position;

            while (distance.sqrMagnitude <= sqrDistanceForNewRandomPoint)
            {
                SetRandomPoint();
                distance = randomPoint - (Vector2)transform.position;
            }
        }

        Vector2 direction = distance.normalized;

        movement.Move(direction);
    }

    private void SetRandomPoint()
    {
        Bounds bounds = GameManager.Instance.collider.bounds;

        randomPoint = new Vector2(
            Random.Range(bounds.min.x + minDistanceFromEdge, bounds.max.x - minDistanceFromEdge),
            Random.Range(bounds.min.y + minDistanceFromEdge, bounds.max.y - minDistanceFromEdge)
        );
    }
}
