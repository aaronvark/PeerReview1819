using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GroundedChecker : MonoBehaviour
{
    public bool Grounded;

    private void OnTriggerEnter(Collider other)
    {
        if(other.tag == "Ground" || other.tag == "Box")
        {
            Grounded = true;
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.tag == "Ground" || other.tag == "Box")
        {
            Grounded = false;
        }
    }
}
