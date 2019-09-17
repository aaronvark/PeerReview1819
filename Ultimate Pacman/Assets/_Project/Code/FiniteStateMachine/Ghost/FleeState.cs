// Check Flee script for comments

using UnityEngine;

public class FleeState : AbstractState
{
    [SerializeField]
    private float speedMultiplier = .5f;

    private Transform player = null;
    private Movement2D movement = null;

    public FleeState(Transform transform) : base(transform) { }

    public override void OnStateEnter()
    {
        player = Player.Instance.transform;
        movement = transform.GetComponent<Movement2D>();

        movement.moveSpeed *= speedMultiplier;
    }

    public override void OnStateUpdate()
    {
        Vector2 distance = transform.position - player.position;

        movement.Move(distance);
    }

    public override void OnStateExit()
    {
        movement.moveSpeed /= speedMultiplier;
    }
}
