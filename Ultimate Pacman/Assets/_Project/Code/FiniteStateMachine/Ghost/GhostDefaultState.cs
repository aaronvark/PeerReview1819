using UnityEngine;

// Set up the main components the GhostDefaultState will always need to function correctly
public class GhostDefaultState : AbstractState
{
    protected Transform player = null;
    protected Movement2D movement = null;

    public GhostDefaultState(Transform transform) : base(transform) { }

    public override void OnStateEnter()
    {
        // Enable all colliders on this gameobject
        Collider2D[] playerColliders = transform.GetComponentsInChildren<Collider2D>();
        for (int i = 0; i < playerColliders.Length; i++)
            playerColliders[i].enabled = true;

        transform.GetComponent<Ghost>().animator.ResetTrigger("SetVulnerable");

        player = Player.Instance.transform;
        movement = transform.GetComponent<Movement2D>();
    }
}
