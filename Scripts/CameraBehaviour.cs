using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraBehaviour : MonoBehaviour
{
    public static CameraBehaviour Instance; //PUUR VOOR TESTEN
    public int CurrentType = 1; //PUUR VOOR TESTEN

    [SerializeField] private float movementSpeed = 10;
    private Camera cam;

    private void Awake() {
        Instance = this; //PUUR VOOR TESTEN
        cam = GetComponent<Camera>();
    }

    private void Update() {

        HandleMovement();

        if (Input.GetKeyDown(KeyCode.Alpha1)) CurrentType = 1; //PUUR VOOR TESTEN
        if (Input.GetKeyDown(KeyCode.Alpha2)) CurrentType = 2; //PUUR VOOR TESTEN
    }

    private void HandleMovement() {

        if (Input.GetKey(KeyCode.W)) transform.Translate(Vector3.up * movementSpeed / 100);
        if (Input.GetKey(KeyCode.A)) transform.Translate(Vector3.left * movementSpeed / 100);
        if (Input.GetKey(KeyCode.S)) transform.Translate(Vector3.down * movementSpeed / 100);
        if (Input.GetKey(KeyCode.D)) transform.Translate(Vector3.right * movementSpeed / 100);

        if (Input.mouseScrollDelta.y > 0) cam.orthographicSize = Mathf.Clamp(cam.orthographicSize - 0.5f, 1, 30);
        if (Input.mouseScrollDelta.y < 0) cam.orthographicSize = Mathf.Clamp(cam.orthographicSize + 0.5f, 1, 30);
    }
}
