using System;
using UnityEngine;

/// <summary>
/// Camera management class/ manages all camera related behaviour
/// </summary>
public class CameraManager : MonoBehaviour
{
    /// <summary>
    /// the speed the camera uses when moving
    /// </summary>
    [SerializeField] private float cameraSpeed = 5f;
    public float CameraSpeed { get => cameraSpeed; set => cameraSpeed = value; }

    /// <summary>
    /// the amount of distance the camera should travel
    /// </summary>
    [SerializeField] private float cameraTravelDistance = 50f;
    public float CameraTravelDistance { get => cameraTravelDistance; set => cameraTravelDistance = value; }

    /// <summary>
    /// reference to the first main camera in the scene
    /// </summary>
    private Camera MainCamera { get; set; }

    /// <summary>
    /// last known camera position
    /// </summary>
    private Vector3 CurrentCameraPosition { get; set; }

    // Start is called before the first frame update
    void Awake()
    {
        //get the main camera reference from this scene
        MainCamera = Camera.main;

        //add the ResetCamera method ( subscription ) to the gameUpdateEvent EVENT
        EventManagerGen<float>.AddHandler(EVENT.gameUpdateEvent, ResetCamera);

        //add add the ResetCamera method ( subscription)  to the reloadGame EVENT
        EventManagerGen<float>.AddHandler(EVENT.reloadGame, ResetCamera);

        //add the ResetCamera method ( subscription ) to the initializeGame EVENT
        EventManagerGen<float>.AddHandler(EVENT.initializeGame, ResetCamera);
    }

    //we might need this function later on
    /*
    public void UpdateCamera()
    {
        mainCamera.transform.LerpTransform(new Vector3(mainCamera.transform.position.x + cameraTravelDistance, mainCamera.transform.position.y, mainCamera.transform.position.z), cameraSpeed);
    }*/

    /// <summary>
    /// We reset the camera.x to xPosition
    /// </summary>
    /// <param name="xPosition"></param>
    public void ResetCamera(float xPosition)
    {
        MainCamera = FindObjectOfType<Camera>();
        if (MainCamera == null) return;

        MainCamera.transform.LerpTransform(this, new Vector3(xPosition, MainCamera.transform.position.y, MainCamera.transform.position.z), CameraSpeed);

    }


}
