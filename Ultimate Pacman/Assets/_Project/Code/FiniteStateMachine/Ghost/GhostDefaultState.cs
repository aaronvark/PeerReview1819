using UnityEngine;
using System.Collections;

public class GhostDefaultState : AbstractState
{
    protected Transform player = null;
    protected Movement2D movement = null;

    public GhostDefaultState(Transform transform) : base(transform) { }

    public override void OnStateEnter()
    {
        Collider2D[] playerColliders = transform.GetComponentsInChildren<Collider2D>();
        for (int i = 0; i < playerColliders.Length; i++)
            playerColliders[i].enabled = true;

        player = Player.Instance.transform;
        movement = transform.GetComponent<Movement2D>();
    }
}
