using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerCollisions : AbstractAvatarClass
{
    private void OnCollisionEnter(Collision collision)
    {
        if(collision.gameObject.tag == "Enemy")
        {
            TakeDamage(collision.gameObject.GetComponent<Enemy>().Damage);
            return;
        }
    }
}
