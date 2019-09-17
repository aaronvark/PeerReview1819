using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Bas.Interfaces;

public class Projectile : PoolableBehaviour, IPoolable
{
    public float speed;
    public int damage;

    private float timeRemaining = 2;

    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.tag == "Enemy")
        {
            Enemy _enemy = collision.gameObject.GetComponent<Enemy>();
            _enemy.SplitEnemy();
            _enemy.TakeDamage(damage);
            EventManager.OnLevelUpdateHandler();

            EventManager.OnScoreChangedHandler(_enemy.enemyInput.points);
            //Recycle();
            gameObject.SetActive(false);
        }
        if (collision.gameObject.tag != "Projectile")
        {
            //Recycle();
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
            //Recycle();
            gameObject.SetActive(false);

        }
    }
}
