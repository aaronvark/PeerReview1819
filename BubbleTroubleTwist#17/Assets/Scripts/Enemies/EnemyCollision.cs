using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Bas.Interfaces;

/// <summary>
/// Handles the collisions of each enemy 
/// </summary>
public class EnemyCollision : AbstractAvatarClass
{
    public float collisionCooldownTime = 2f;

    private bool collisionReadyCheck = true;

    private void OnCollisionEnter(Collision collision)
    {
        var checkForDamagableObject = collision.gameObject.GetComponent<IDamagable<int>>();
        if (checkForDamagableObject != null && collisionReadyCheck)
        {
            StartCoroutine(CollisionReady());

            EventManager.OnPlayerHitHandler(damage);
            //checkForDamagableObject.TakeDamage(damage);
        }
    }

    private IEnumerator CollisionReady()
    {
        collisionReadyCheck = false;
        yield return new WaitForSeconds(collisionCooldownTime);
        collisionReadyCheck = true;
    }
}
