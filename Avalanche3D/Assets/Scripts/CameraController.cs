using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraController : MonoBehaviour
{
    GameManager gameManager;

    Transform target;

    public Vector3 Offset;

    public float RotationSpeed;

    public Transform Pivot;

    // Start is called before the first frame update
    void Start()
    {
        gameManager = GameManager.Instance;
        target = gameManager.Player.transform;
        Offset = target.position - transform.position;
        Pivot.transform.position = target.position;
        Pivot.transform.parent = target;

        Cursor.lockState = CursorLockMode.Locked;

    }

    // Update is called once per frame
    void LateUpdate()
    {
        MoveAndRotate();
    }


    void MoveAndRotate()
    {
        //Get X position of mouse and rotate
        float horizontal = Input.GetAxis("Mouse X") * RotationSpeed;
        float vertical = Input.GetAxis("Mouse Y") * RotationSpeed;

        target.Rotate(0, horizontal, 0);
        Pivot.Rotate(-vertical, 0, 0);

        float desiredXAngle = Pivot.eulerAngles.x;
        float desiredYAngle = target.eulerAngles.y;

        Quaternion rotation = Quaternion.Euler(desiredXAngle, desiredYAngle, 0);
        transform.position = target.position - (rotation * Offset);

        CheckMaxPosition();

        transform.LookAt(target);
    }

    void CheckMaxPosition()
    {
        if(transform.position.y < target.position.y)
        {
            transform.position = new Vector3(transform.position.x, target.position.y -0.5f, transform.position.z);
        }
    }
}
