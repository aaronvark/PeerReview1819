using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy : Actor
{ 
    public override void Death()
    {
        Destroy(gameObject);
    }

    private void OnCollisionStay2D(Collision2D collision)
    {
        if (collision.gameObject.name == "Player")
        {
            Death();
        }
    }
}
