using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Bas.Interfaces;

public class EnemyCollision : AbstractAvatarClass
{
    private void OnCollisionEnter(Collision collision)
    {
        var checkForDamagableObject = collision.gameObject.GetComponent<IDamagable<int>>();
        if (checkForDamagableObject != null)
        {
            EventManager.OnPlayerHitHandler(damage);
            //checkForDamagableObject.TakeDamage(damage);
        }
    }
}
