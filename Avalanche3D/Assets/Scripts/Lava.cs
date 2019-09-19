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
    private bool canDamage;

    private bool rising;

    private void Awake()
    {
        canDamage = true;
        rising = false;
        InstanceManager<GameManager>.GetInstance("GameManager").onDeath += OnPlayerDeath;
        InstanceManager<GameManager>.GetInstance("GameManager").onStartGame += OnStartGameLava;

    }

    private void Update()
    {
        if(rising)
        {
            Rise();
        }
    }

    private void Rise()
    {
        //TODO-Optional: Add logic for speeding up over time.
        transform.Translate(new Vector3(0, RiseSpeed / 100, 0));
    }

    private void OnStartGameLava()
    {
        rising = true;
        Cursor.lockState = CursorLockMode.Locked;
    }

    void OnPlayerDeath()
    {
        rising = false;
        canDamage = false;
        StopAllCoroutines();
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
            canDamage = true;
        }
    }

    private IEnumerator Damage(IDamagable damageTaker)
    {
        if(canDamage)
        {
            damageTaker.TakeDamage(DamageGiven);
            canDamage = false;
            yield return new WaitForSeconds(DamageInterval);
            canDamage = true;
            StartCoroutine(Damage(damageTaker));
        }
        else
        {
            yield break;
        }
    }
}
