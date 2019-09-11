using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Projectile : PoolableBehaviour
{
    public float speed;
    public int damage;

    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.tag == "Enemy")
        {
            Enemy _enemy = collision.gameObject.GetComponent<Enemy>();
            _enemy.SplitEnemy();
            _enemy.TakeDamage(damage);
            EventManager.Broadcast(EVENT.MyEvent2);
            Recycle();
            //gameObject.SetActive(false);
        }
        if (collision.gameObject.tag != "Projectile")
        {
            Recycle();
            //gameObject.SetActive(false);
        }
    }
}
