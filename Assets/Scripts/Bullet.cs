using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bullet : MonoBehaviour
{
    public int bulletDamage;

    private void OnCollisionEnter(Collision collision)
    {
        try
        {
            collision.gameObject.GetComponent<IDamagable>().Damage(bulletDamage);
        }
        catch { }
    }
}
