﻿using UnityEngine;

public class Asteroid : FloatingEntity , IDestroyable
{
    [SerializeField]
    private int willBeDevidedBy = 2;

    private int health = 3;

    public override void OnObjectReuse()
    {
        transform.Rotate(new Vector3(0, 0, Random.Range(0, 360)));
        base.OnObjectReuse();
    }
    public void TakeDamage(int val)
    {
        health-= val;
        if (health != 0)
        {
            Split();
        }
        base.Destroy();
    }

    //when hit by a bullet
    public void Split()
    {
        for (int i = 0; i < willBeDevidedBy; i++) {
            AsteroidSpawner.Instance.SpawnAsteroid(transform.position, Health);
        }
    }

    //bounces from the walls
    private void OnCollisionEnter2D(Collision2D _collision)
    {
        Collider2D _collider = _collision.collider;
        if (_collision.gameObject.GetComponent<Character>())
        {
            _collision.gameObject.GetComponent<Character>().TakeDamage(1);
        }
    }

    //scales the size according to the value
    public int Health
    {
        get {
            return health;
        }
        set {
            health = value;
            transform.localScale = new Vector2(health * 2, health * 2);
        }
    }
}
