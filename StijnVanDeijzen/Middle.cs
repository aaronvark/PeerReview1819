using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Middle : MonoBehaviour
{
    public void UpdateMiddle()
    {
        transform.Rotate(new Vector3(0, 0, GameManager.Instance.middleRotateSpeed * 1));

        List<Block> blocks = GameManager.Instance.GetBlocks();
        if (blocks != null)
        {
            foreach (Block block in blocks)
            {
                block.GetRigidB().AddForce(-block.transform.position.normalized * GameManager.Instance.middleForce);
            }
        }
    }
}