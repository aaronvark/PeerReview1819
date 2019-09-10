using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DestructableWall : MonoBehaviour, IDamagable
{
    [HideInInspector] public float waitForDestroy;

    public void Damage()
    {
        StartCoroutine(WaitToDestroy());
    }

    public void Die()
    {
        Debug.Log("Wall Destroyed");
        WallManager.Instance.RemoveFromList(this);
        Destroy(this.gameObject);
    }

    private IEnumerator WaitToDestroy()
    {
        yield return new WaitForSeconds(waitForDestroy);
        Die();
    }
}
