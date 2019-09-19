using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    [SerializeField]
    private float movementSpeed;
    [SerializeField]
    private float rotationSpeed;
    private Rigidbody2D rb;
    [SerializeField]
    private Camera cam;
    [SerializeField]
    private GameObject[] thrusters;

    private void Start()
    {
        rb = GetComponent<Rigidbody2D>();
    }

    private void Update()
    {
        if (GameManager.Instance.gameIsPlaying)
        {
            // Player Rotation
            if (Input.GetAxis("Horizontal") != 0)
            {
                float _angle = (transform.rotation.z + (Input.GetAxis("Horizontal") * -rotationSpeed));
                transform.rotation = Quaternion.Euler(transform.rotation.eulerAngles + new Vector3(0, 0, _angle));
            }
            // Thruster particles
            if (Input.GetAxis("Vertical") != 0)
            {
                foreach (GameObject _thruster in thrusters)
                {
                    if (!_thruster.activeSelf)
                    {
                        _thruster.SetActive(true);
                    }
                }
            }
            else
            {
                foreach (GameObject _thruster in thrusters)
                {
                    if (_thruster.activeSelf)
                    {
                        _thruster.SetActive(false);
                    }
                }
            }

            Vector3 _tempLeft = cam.ScreenToWorldPoint(new Vector3(0, 0, cam.nearClipPlane));
            Vector3 _tempRight = cam.ScreenToWorldPoint(new Vector3(cam.pixelWidth, cam.pixelHeight, cam.nearClipPlane));
            if (transform.position.x > _tempRight.x || transform.position.x < _tempLeft.x)
            {
                Vector3 _tempPlayerPos = transform.position;
                _tempPlayerPos.x = -_tempPlayerPos.x;
                transform.position = _tempPlayerPos;
            }
            if (transform.position.y > _tempRight.y || transform.position.y < _tempLeft.y)
            {
                Vector3 _tempPlayerPos = transform.position;
                _tempPlayerPos.y = -_tempPlayerPos.y;
                transform.position = _tempPlayerPos;
            }
        }
    }
    // Player thrust
    private void FixedUpdate()
    {
        if (GameManager.Instance.gameIsPlaying)
        {
            Vector2 _tempSpeed = new Vector2(0f, (Input.GetAxis("Vertical") * movementSpeed));
            rb.AddRelativeForce(_tempSpeed);
        }
    }
}
