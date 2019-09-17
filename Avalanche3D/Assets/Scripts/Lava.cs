using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Lava : MonoBehaviour
{
    //Public variables
    public float DamageGiven;
    public float DamageInterval;
    public float RiseSpeed;

    //Private variables
    private bool CanDamage;

    private void Awake()
    {
        CanDamage = true;
    }

    private void Update()
    {
        Rise();
    }

    private void Rise()
    {
        //TODO-Optional: Add logic for speeding up over time.
        transform.Translate(new Vector3(0, RiseSpeed / 100, 0));
    }

    private void OnTriggerEnter(Collider other)
    {
        IDamagable damageTaker = other.GetComponent<IDamagable>();
        if(damageTaker != null)
        {
            StartCoroutine(Damage(damageTaker));
        }
    }

    private void OnTriggerExit(Collider other)
    {
        IDamagable damageTaker = other.GetComponent<IDamagable>();
        if (damageTaker != null)
        {
            StopAllCoroutines();
            CanDamage = true;
        }
    }

    private IEnumerator Damage(IDamagable damageTaker)
    {
        if(CanDamage)
        {
            damageTaker.TakeDamage(DamageGiven);
            CanDamage = false;
            yield return new WaitForSeconds(DamageInterval);
            CanDamage = true;
            StartCoroutine(Damage(damageTaker));
        }
        else
        {
            yield break;
        }
    }
}
