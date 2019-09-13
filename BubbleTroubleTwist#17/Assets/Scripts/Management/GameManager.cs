using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public delegate void OnGameUpdate();

public class GameManager : MonoBehaviour
{
    private void Start()
    {
        //EventManager.AddHandler(EVENT.gameUpdateEvent, UpdateGame);
        //EventManager.AddHandler(EVENT.gameUpdateEvent, UpdateUI);
        EventManager.OnGameOverHandler += GameOver;
    }

    public void UpdateGame()
    {

    }

    public void UpdateUI()
    {

    }

    public void GameOver()
    {
        SceneManager.LoadScene(0);
    }
}
