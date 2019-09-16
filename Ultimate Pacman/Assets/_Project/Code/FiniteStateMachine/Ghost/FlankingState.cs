using System.Linq;
using UnityEngine;
using Extensions;

public class FlankingState : GhostDefaultState
{
    private Transform blinky = null;

    public FlankingState(Transform transform) : base(transform) { }

    public override void OnStateEnter()
    {
        base.OnStateEnter();

        blinky = GameManager.Instance.GetGhosts.First(g => g is Blinky).transform;
    }

    public override void OnStateUpdate()
    {
        Vector2 playerPoint = (Vector2)player.position + (Vector2)player.right * 2f;
        Vector2 flankingPoint = (Vector2)blinky.position + ((playerPoint - (Vector2)blinky.position) * 2f);
        Vector2 distance = flankingPoint - (Vector2)transform.position;
        Vector2 direction = distance.normalized;

        movement.Move(direction);
    }
}
