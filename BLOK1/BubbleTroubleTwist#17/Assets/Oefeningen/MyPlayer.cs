using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MyPlayer : ActorBase
{
    public GameObject bullet;
    public Transform firePoint;
    public float shootSpeed = 5f;
    Rigidbody rBody;

    private void Awake()
    {
        if (rBody == null)
            rBody = GetComponent<Rigidbody>();
    }

    private void Update()
    {
        if (Input.GetMouseButtonDown(0))
            Attack();
    }

    private void FixedUpdate()
    {
        Move();
    }

    public override void Move()
    {
        base.Move();
        float _vertical = Input.GetAxis("Vertical");
        float _horizontal = Input.GetAxis("Horizontal");
        rBody.velocity = new Vector3(_horizontal * speed, rBody.velocity.y, _vertical * speed).normalized;
    }

    public override void Attack()
    {
        base.Attack();
        GameObject bull = Instantiate(bullet, firePoint.position, Quaternion.identity);
        bull.GetComponent<Bullet>().shootSpeed = shootSpeed;
    }

    private void OnDestroy()
    {
        OnDeathHandler -= OnDeath;
    }
}
