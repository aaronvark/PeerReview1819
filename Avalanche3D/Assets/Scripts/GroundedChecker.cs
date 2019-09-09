using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GroundedChecker : MonoBehaviour
{
    // Public Variables
    public bool Grounded;
    public bool Walled;
    public Vector3 Opposite;

    private void OnTriggerEnter(Collider other)
    {
        if (other.tag == "Ground")
        {
            Grounded = true;
        }
        if (other.tag == "Box")
        {
            Walled = true;
            Opposite = (transform.position - other.transform.position);
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.tag == "Ground")
        {
            Grounded = false;
        }
        if (other.tag == "Box")
        {
            Walled = false;
            Opposite = (transform.position - other.transform.position);
        }
    }
}
