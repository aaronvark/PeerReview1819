using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Actor : MonoBehaviour, IDamagable
{
    public int gameID;
    public int health = 3;
    public GameObject bombDummy;
    public Bomb bomb;

    public UIManager uiManager;

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

        //Player zn gameID is altijd 1
        Debug.Log("ID : " + gameID + " &  health : " + health);
        uiManager.DecreaseHearts(gameID, health);
        if (health <= 0)
        {
            Die();
        }
    }

    public virtual void Die()
    {
        Debug.Log("Game Over");
    }

    public void DeployBomb()
    {
        bombDummy.SetActive(true);
        bombDummy.transform.position = transform.position;
    }

    private float Timer(float _timer)
    {
        return _timer -= Time.deltaTime;
    }
}
