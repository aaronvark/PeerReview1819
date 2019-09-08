using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public delegate void OnGameUpdate();

public class GameManager : MonoBehaviour
{
    public OnGameUpdate onGameUpdateHandler;
    public float cameraSpeed = 5f;
    private Camera mainCamera;

    private void Start()
    {
        mainCamera = Camera.main;
        EventManager.AddHandler(EVENT.gameUpdateEvent, UpdateGame);
        EventManager.AddHandler(EVENT.gameUpdateEvent, UpdateUI);
    }

    public void UpdateGame()
    {
        mainCamera.transform.LerpTransform(new Vector3(mainCamera.transform.position.x + 50, mainCamera.transform.position.y, mainCamera.transform.position.z), cameraSpeed);
    }

    public void UpdateUI()
    {

    }
}
