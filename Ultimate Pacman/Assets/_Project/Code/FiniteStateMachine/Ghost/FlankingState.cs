using System.Linq;
using UnityEngine;

public class FlankingState : GhostDefaultState
{
    private const float distanceInFrontOfPlayer = 2f;
    private const float flankingPointMultiplier = 2f;

    private Transform blinky = null;

    public FlankingState(Transform transform) : base(transform) { }

    public override void OnStateEnter()
    {
        base.OnStateEnter();

        blinky = GameManager.Instance.GetGhosts.First(g => g is Blinky).transform;
    }

    public override void OnStateUpdate()
    {
        // Find the point flankingPointMultiplier times farther than from Blinky (Red ghost) to the player (Pacman)
        Vector2 playerPoint = (Vector2)player.position + (Vector2)player.right * distanceInFrontOfPlayer;
        Vector2 blinkyPoint = (Vector2)blinky.position;
        Vector2 flankingPoint = blinkyPoint + ((playerPoint - blinkyPoint) * flankingPointMultiplier);

        Vector2 distance = flankingPoint - (Vector2)transform.position;

        movement.Move(distance);
    }
}
