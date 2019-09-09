using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public delegate void OnGameUpdate();

public class GameManager : MonoBehaviour
{
    private void Start()
    {
        EventManager.AddHandler(EVENT.gameUpdateEvent, UpdateGame);
        EventManager.AddHandler(EVENT.gameUpdateEvent, UpdateUI);
    }

    public void UpdateGame()
    {
    }

    public void UpdateUI()
    {

    }
}
