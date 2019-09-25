using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RotateToMouse : MonoBehaviour
{
    float speed = 5f;
    private void Update()
    {
        Vector3 worldPos = Camera.main.ScreenToWorldPoint(Input.mousePosition);
        transform.transform.rotation = Quaternion.RotateTowards(transform.rotation, Quaternion.LookRotation(worldPos - transform.position), speed * Time.deltaTime);

    }


}
