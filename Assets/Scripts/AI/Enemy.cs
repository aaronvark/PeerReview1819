using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy : MonoBehaviour, IDamagable
{

    [Header("Move Settings")]
    [Tooltip("The distance from the player for which it stops.")]
    [SerializeField] float stopDistance = 200f;
    [SerializeField] float moveSpeed = 10f;

    [Header("Health Settings")]
    [SerializeField] int health = 1000;

    private GameObject target;

    // Start is called before the first frame update
    void Start()
    {
        target = GameObject.FindGameObjectWithTag("Player");
    }

    // Update is called once per frame
    void Update()
    {
        MoveToTarget();
    }

    void MoveToTarget()
    {
        float _targetDistance = Vector3.Distance(transform.position, target.transform.position);

        if(_targetDistance >= stopDistance){
            transform.position = Vector3.Lerp(transform.position, target.transform.position, Time.deltaTime * moveSpeed * 0.01f);
        }
    }

    public void Damage(int damage)
    {
        if (health <= 0) {
            Die();
        }
        else{
            Die();
        }
    }

    public void Die()
    {
        Destroy(this.gameObject);
    }
}
