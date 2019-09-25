using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Bas.Interfaces;

public class Projectile : PoolableBehaviour, IPoolable
{
    private int damage;

    private float timeRemaining = 2;

    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.tag == "Enemy")
        {
            Enemy _enemy = collision.gameObject.GetComponent<Enemy>();
            _enemy.SplitEnemy();
            _enemy.TakeDamage(damage);
            EventManager.OnLevelUpdateHandler();
            EventManager.OnScoreChangedHandler(_enemy.enemyInput.Points);
            gameObject.SetActive(false);
        }
        if (collision.gameObject.tag != "Projectile")
        {
            gameObject.SetActive(false);
        }
    }

    private void Update()
    {
        if(timeRemaining > 0)
        {
            timeRemaining -= Time.deltaTime;
        }
        else
        {
            timeRemaining = 2;
            gameObject.SetActive(false);
        }
    }

    public void SetDamage(int _damage)
    {
        damage = _damage;
    }
}
