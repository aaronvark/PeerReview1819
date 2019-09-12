using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraManager : MonoBehaviour
{
    public float cameraSpeed = 5f;
    public float cameraTravelDistance = 50f;

    private Camera mainCamera;
    private Vector3 currentCameraPosition;

    // Start is called before the first frame update
    void Awake()
    {
        mainCamera = Camera.main;

        //using untiy system.Action()
        EventManagerGen<float>.AddHandler(EVENT.gameUpdateEvent, ResetCamera);
        //using unity delegate
        //EventManager.OnSaveLevelHandler += ResetCamera;

        EventManagerGen<float>.AddHandler(EVENT.reloadGame, ResetCamera);
    }

    public void UpdateCamera()
    {
        mainCamera.transform.LerpTransform(new Vector3(mainCamera.transform.position.x + cameraTravelDistance, mainCamera.transform.position.y, mainCamera.transform.position.z), cameraSpeed);
    }

    public void ResetCamera(float xPosition)
    {
        mainCamera.transform.LerpTransform(new Vector3(xPosition, mainCamera.transform.position.y, mainCamera.transform.position.z), cameraSpeed);
    }
}
