using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Gobly : ActorBase
{
    private void FixedUpdate()
    {
        Move();
    }
    public override void Move()
    {
        base.Move();
        transform.position = new Vector3(transform.position.x, transform.position.y, Mathf.PingPong(Time.time, 3));

    }
    private void OnDestroy()
    {
        OnDeathHandler -= OnDeath;
    }
}
