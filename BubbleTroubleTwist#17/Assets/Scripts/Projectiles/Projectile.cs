using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Bas.Interfaces;

public class Projectile : PoolableBehaviour
{
    public float speed;
    public int damage;

    private float timeRemaining = 4;

    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.tag == "Enemy")
        {
            Enemy _enemy = collision.gameObject.GetComponent<Enemy>();
            _enemy.SplitEnemy();
            _enemy.TakeDamage(damage);
            EventManager.OnLevelUpdateHandler();
            //Recycle();
            //gameObject.SetActive(false);
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
            timeRemaining = 4;
            //Recycle();
            gameObject.SetActive(false);

        }
    }
}
