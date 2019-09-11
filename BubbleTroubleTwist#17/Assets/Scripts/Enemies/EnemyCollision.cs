using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyCollision : AbstractAvatarClass
{
    private void OnCollisionEnter(Collision collision)
    {
        if(collision.gameObject.tag == "Players")
        {
            EventManager.OnPlayerHitHandler(damage);
        }
    }
}
