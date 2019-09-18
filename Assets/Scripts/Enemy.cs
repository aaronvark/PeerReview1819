using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy : Actor
{ 
    public override void Death()
    {
        //Adds score.
        GameObject.FindGameObjectWithTag("UI").GetComponent<UiHandler>().ScoreUp();

        //Self destruct.
        Destroy(gameObject);
    }

    //Sets the direction and velocity for the enemy.
    private void Start()
    {
        Vector3 diff = Vector3.zero - transform.position;
        diff.Normalize();

        float rot_z = Mathf.Atan2(diff.y, diff.x) * Mathf.Rad2Deg;

        float _directionModifier = Random.Range(-45f, 45f);

        transform.rotation = Quaternion.Euler(0f, 0f, rot_z - 90 + _directionModifier);

        diff = transform.rotation * Vector3.one * Random.Range(1f, 4f);

        GetComponent<Rigidbody2D>().velocity = diff;
    }

    //Roept de death functie aan als de enemy collide met de speler. 
    private void OnCollisionStay2D(Collision2D _collision)
    {
        if (_collision.gameObject.name == "Player")
        {
            Death();
        }
    }
}
