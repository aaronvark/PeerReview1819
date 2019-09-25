using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Bas.Interfaces;

/*
public class Bullet : MonoBehaviour
{
    public void Spawn(Vector3 position)
    {
        gameObject.SetActive(true);
        gameObject.transform.position = position;
    }
}*/

public class BulletPool : GenericObjectPool<Projectile>
{
    /*
    public void SpawnExplosion(Vector3 position)
    {
        var explosion = this.Get();
        if (explosion == null)
        {
            // The pool is empty, so we can't spawn any more at the moment.
            return;
        }

        explosion.Spawn(position);
        m_activeExplosions.Add(explosion);
    }*/
}
