using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DamagableEntity : MonoBehaviour, IDamagable
{
    public float MaxHealth;
    float currentHealth;

    private void Awake()
    {
        currentHealth = MaxHealth;
    }

    public void TakeDamage(float damage)
    {
        currentHealth -= damage;
        if(currentHealth <= 0)
        {
            Die();
        }
    }

    protected virtual void Die()
    {
        //This Method is meant to be overridden
    }


}
