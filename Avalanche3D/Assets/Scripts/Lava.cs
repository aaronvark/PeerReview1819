using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Lava : MonoBehaviour
{
    public float DamageGiven;
    public float DamageInterval;

    bool CanDamage;

    private void Awake()
    {
        CanDamage = true;
    }

    private void OnTriggerEnter(Collider other)
    {
        IDamagable damageTaker = other.GetComponent<IDamagable>();
        if(damageTaker != null)
        {
            StartCoroutine(Damage(damageTaker));
        }
    }

    IEnumerator Damage(IDamagable damageTaker)
    {
        if(CanDamage)
        {
            damageTaker.TakeDamage(DamageGiven);
            CanDamage = false;
            yield return new WaitForSeconds(DamageInterval);
            CanDamage = true;         
        }
        else
        {
            yield break;
        }
    }
}
