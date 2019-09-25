using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Bas.Interfaces;

public class Wall : MonoBehaviour, IDamagable<int>
{
    public float health = 10f;
    public event OnDeath OnDeathHandler;

    private void Start()
    {
        OnDeathHandler += OnDeath;
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
