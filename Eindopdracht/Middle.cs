using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Middle : MonoBehaviour
{
    void Start()
    {
        
    }

    void FixedUpdate()
    {
        transform.Rotate(new Vector3(0,0, GameManager.Instance.middleRotateSpeed * 1));

        foreach( Block block in GameManager.Instance.blocks)
        {
            block.rigidB.AddForce(-block.transform.position * GameManager.Instance.middleForce);
        }
    }
}
