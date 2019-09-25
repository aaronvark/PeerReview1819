using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpawnerHeightChanger : MonoBehaviour
{
    private void OnTriggerStay(Collider other)
    {
        if (other.tag == "Box")
        {
            if (other.GetComponent<Box>().IsPlaced)
            {
                transform.parent.position = new Vector3(transform.parent.position.x, transform.parent.position.y + 1, transform.parent.position.z);
            }
        }
    }
}
