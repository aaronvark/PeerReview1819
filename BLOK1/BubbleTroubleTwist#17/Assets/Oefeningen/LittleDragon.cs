using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LittleDragon : ActorBase
{
    private void FixedUpdate()
    {
        Move();
    }
    public override void Move()
    {
        base.Move();
        transform.position = new Vector3(Mathf.PingPong(Time.time, 3), transform.position.y, transform.position.z);

    }
    private void OnDestroy()
    {
        OnDeathHandler -= OnDeath;
    }
}
