using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// The camera repositions the camera to give the player gthe feeling of an endless area.
/// </summary>
public class CameraLoop : MonoBehaviour
{
    [SerializeField]
    private Transform player;
    [SerializeField]
    private Transform cameraTransform;

    private Camera cam;
    private float camWidth;
    private float camHeight;

    private float widthDevideOffset = 4.5f;
    private float heigthDevideOffset = 1.1f;
    private void Start()
    {
        cam = Camera.main;
        camHeight = 2f * cam.orthographicSize;
        camWidth = camHeight * cam.aspect;

    }
    void Update()
    {
        cameraTransform.position = new Vector3(player.position.x + transform.position.x - camWidth / widthDevideOffset, player.position.y + transform.position.y + camHeight * heigthDevideOffset, -7f);
    }

}
