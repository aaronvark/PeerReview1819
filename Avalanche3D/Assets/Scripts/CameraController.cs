using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraController : MonoBehaviour
{
    //Scripts
    public GameManager GameManagerInstance { get; private set;}

    //References
    private Transform target;
    public Transform Pivot;

    //Public Variables
    public Vector3 Offset;
    public float RotationSpeed;


    // Start is called before the first frame update
    private void Start()
    {
        GameManagerInstance = InstanceManager<GameManager>.GetInstance("GameManager");

        target = GameManagerInstance.Player.transform;
        Offset = target.position - transform.position;
        Pivot.transform.position = target.position;
        Pivot.transform.parent = target;
    }

    // Update is called once per frame
    private void LateUpdate()
    {
        MoveAndRotate();
    }


    private void MoveAndRotate()
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

    private void CheckMaxPosition()
    {
        if (transform.position.y < target.position.y)
        {
            transform.position = new Vector3(transform.position.x, target.position.y - 0.5f, transform.position.z);
        }
    }
}
