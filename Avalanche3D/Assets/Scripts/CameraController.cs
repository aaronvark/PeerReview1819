using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraController : MonoBehaviour
{
    GameManager gameManager;

    Transform target;

    public Vector3 Offset;

    public float rotationSpeed;

    // Start is called before the first frame update
    void Start()
    {
        gameManager = GameManager.Instance;
        target = gameManager.Player.transform;
        Offset = target.position - transform.position;
    }

    // Update is called once per frame
    void Update()
    {
        MoveAndRotate();
    }


    void MoveAndRotate()
    {
        //Get X position of mouse and rotate
        float horizontal = Input.GetAxis("Mouse X") * rotationSpeed;
        float vertical = Input.GetAxis("Mouse Y") * rotationSpeed;

        target.Rotate(-vertical, horizontal, 0);

        float desiredXAngle = target.eulerAngles.x;
        float desiredYAngle = target.eulerAngles.y;


        Quaternion rotation = Quaternion.Euler(desiredXAngle, desiredYAngle, 0);
        transform.position = target.position - (rotation * Offset);

        transform.LookAt(target);
    }
}
