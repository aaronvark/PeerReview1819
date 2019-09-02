using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Bas.Interfaces;

public abstract class AbstractAvatarClass : MonoBehaviour, IDamagable<int>
{
    public int damage;
    public float speed;
    public int health;

    public virtual void OnDeath()
    {
        throw new System.NotImplementedException();
    }

    public virtual bool TakeDamage(int _damage)
    {
        if (health > _damage) { health -= _damage; return true; }
        else return false;
        
    }
}
