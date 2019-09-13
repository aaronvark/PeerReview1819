using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Actor : MonoBehaviour, IDamagable
{
    public int gameID;
    public int health = 3;
    public GameObject bombDummy;
    public Bomb bomb;

    protected UIManager uiManager;

    private void Start()
    {
        uiManager = FindObjectOfType<UIManager>();
    }

    // Update is called once per frame
    private void Update()
    {
        if (Input.GetKeyUp(KeyCode.E) && bomb.bombDeployCheck)
        {
            bomb.Deployed();
            DeployBomb();
        }
    }

    public virtual void Damage()
    {
        health -= 1;
        //Player zn gameID is altijd 1
        uiManager.DecreaseHearts(gameID, this.health);
        if (health <= 0)
        {
            Die();
        }
    }

    public virtual void Die()
    {
        Debug.Log("Game Over");
    }

    protected void DeployBomb()
    {
        bombDummy.SetActive(true);
        bombDummy.transform.position = transform.position;
        //Instantiate(bomb, transform.position, Quaternion.identity);
    }
}
