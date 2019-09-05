using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Bas.Interfaces;

public abstract class ActorBase : MonoBehaviour, IDamagable<int>
{
    public float speed;
    public float health;
    public event OnDeath OnDeathHandler;

    public virtual void Start()
    {
        OnDeathHandler += OnDeath;
    }

    public virtual void Move()
    {

    }

    public virtual void Attack()
    {

    }

    public void OnDeath()
    {
        gameObject.SetActive(false);
        //throw new System.NotImplementedException();
    }

    public virtual bool TakeDamage(int _damage)
    {
        if (health > _damage) { health -= _damage; return true; } else { OnDeathHandler(); return false; }
    }
}
