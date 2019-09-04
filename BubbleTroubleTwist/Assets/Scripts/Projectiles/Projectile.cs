using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Projectile : MonoBehaviour
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
        }
        if (collision.gameObject.tag != "Projectile")
        {
            gameObject.SetActive(false);

        }
    }
}
