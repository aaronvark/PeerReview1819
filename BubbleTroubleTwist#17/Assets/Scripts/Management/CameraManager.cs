using UnityEngine;

/// <summary>
/// Camera management class/ manages all camera related behaviour
/// </summary>
public class CameraManager : MonoBehaviour
{
    /// <summary>
    /// the speed the camera uses when moving
    /// </summary>
    public float cameraSpeed = 5f;

    /// <summary>
    /// the amount of distance the camera should travel
    /// </summary>
    public float cameraTravelDistance = 50f;

    /// <summary>
    /// reference to the first main camera in the scene
    /// </summary>
    private Camera mainCamera;

    /// <summary>
    /// last known camera position
    /// </summary>
    private Vector3 currentCameraPosition;

    // Start is called before the first frame update
    void Awake()
    {
        //get the main camera reference from this scene
        mainCamera = Camera.main;

        //add the ResetCamera method ( subscription ) to the gameUpdateEvent EVENT
        EventManagerGen<float>.AddHandler(EVENT.gameUpdateEvent, ResetCamera);

        //add add the ResetCamera method ( subscription)  to the reloadGame EVENT
        EventManagerGen<float>.AddHandler(EVENT.reloadGame, ResetCamera);
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
        if (mainCamera == null) return;
        mainCamera.transform.LerpTransform(new Vector3(xPosition, mainCamera.transform.position.y, mainCamera.transform.position.z), cameraSpeed);
    }
}
