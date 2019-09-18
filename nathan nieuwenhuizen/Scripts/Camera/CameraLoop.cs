using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraLoop : MonoBehaviour
{
    [SerializeField]
    private Transform player;
    [SerializeField]
    private Transform cameraTransform;

    private Camera cam;
    private float camWidth;
    private float camHeight;
    private void Start()
    {
        cam = Camera.main;
        camHeight = 2f * cam.orthographicSize;
        camWidth = camHeight * cam.aspect;

    }
    void Update()
    {
        cameraTransform.position = new Vector3(player.position.x + transform.position.x - camWidth / 4.5f, player.position.y + transform.position.y + camHeight * 1.1f, -7f);
    }

}
