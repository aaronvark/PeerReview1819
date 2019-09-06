using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Middle : MonoBehaviour
{
    public void UpdateMiddle()
    {
        transform.Rotate(new Vector3(0,0, GameManager.Instance.middleRotateSpeed * 1));

        foreach( Block block in GameManager.Instance.GetBlocks())
        {
            block.GetRigidB().AddForce(-block.transform.position.normalized * GameManager.Instance.middleForce);
        }
    }
}