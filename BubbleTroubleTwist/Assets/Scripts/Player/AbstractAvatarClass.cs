using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Bas.Interfaces;

/// <summary>
/// Base class for all avatars in the game
/// </summary>
public abstract class AbstractAvatarClass : MonoBehaviour, IDamagable<int>
{
    public ObjectPooler objectPooler;

    public int damage;
    public float speed;
    public int health;

    public virtual void Start()
    {
        objectPooler = ObjectPooler.Instance;
    }

    public virtual void OnDeath()
    {
        gameObject.SetActive(false);
        //throw new System.NotImplementedException();
    }

    public virtual bool TakeDamage(int _damage)
    {
        if (health > _damage) { health -= _damage; return true; }
        else { OnDeath(); return false; }        
    }
}
