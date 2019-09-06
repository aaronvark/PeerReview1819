﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Actor : MonoBehaviour, IDamagable
{
    public int health = 3;
    public GameObject bombDummy;
    public Bomb bomb;

    // Update is called once per frame
    private void Update()
    {
        if (Input.GetKeyUp(KeyCode.E) && bomb.bombDeployCheck)
        {
            bomb.Deployed();
            DeployBomb();
        }
    }

    public void Damage()
    {
        health -= 1;
        Debug.Log(health);
        if (health <= 0)
        {
            Die();
        }
    }

    public virtual void Die()
    {
        Debug.Log("Game Over");
    }

    private void DeployBomb()
    {
        bombDummy.SetActive(true);
        bombDummy.transform.position = transform.position;
        //Instantiate(bomb, transform.position, Quaternion.identity);
    }
}
