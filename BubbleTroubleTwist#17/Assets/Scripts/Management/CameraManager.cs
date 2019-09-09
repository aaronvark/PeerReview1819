using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraManager : MonoBehaviour
{
    public float cameraSpeed = 5f;
    public float cameraTravelDistance = 50f;

    private Camera mainCamera;

    // Start is called before the first frame update
    void Start()
    {
        mainCamera = Camera.main;
        EventManager.AddHandler(EVENT.gameUpdateEvent, UpdateCamera);
    }

    public void UpdateCamera()
    {
        mainCamera.transform.LerpTransform(new Vector3(mainCamera.transform.position.x + cameraTravelDistance, mainCamera.transform.position.y, mainCamera.transform.position.z), cameraSpeed);
    }

}
