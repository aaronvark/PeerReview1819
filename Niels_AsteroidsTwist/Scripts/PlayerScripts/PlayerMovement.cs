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

    public void OnDeath()
    {
        GameManager.OnReset();
    }

    private void Start() {
        rb = GetComponent<Rigidbody2D>();
    }

    private void Update() {
        // Player Rotation
        if (Input.GetAxis("Horizontal") != 0) { 
        float _angle = (transform.rotation.z + (Input.GetAxis("Horizontal") * -rotationSpeed));
        transform.rotation = Quaternion.Euler(transform.rotation.eulerAngles + new Vector3(0, 0, _angle));
        }
        // Thruster particles
        if (Input.GetAxis("Vertical") != 0) { 
            foreach(GameObject _thruster in thrusters) { 
                if (!_thruster.activeSelf) { 
                    _thruster.SetActive(true);
                }
            }
        }
        else { 
            foreach (GameObject _thruster in thrusters) { 
                if (_thruster.activeSelf) { 
                    _thruster.SetActive(false);
                }
            }
        }
    }
    // Player thrust
    private void FixedUpdate() {
        Vector2 _tempSpeed = new Vector2(0f, (Input.GetAxis("Vertical") * movementSpeed));
        rb.AddRelativeForce(_tempSpeed);
    }

    private void OnDisable()
    {
        OnDeath();
    }
}
