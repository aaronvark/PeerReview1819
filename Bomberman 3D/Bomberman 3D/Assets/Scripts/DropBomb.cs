using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DropBomb : MonoBehaviour
{
    public GameObject bomb;
    public bool bombDeployPossible = true;

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKey(KeyCode.E) && bombDeployPossible)
        {
            bombDeployPossible = false;
            DeployBomb();
        }
    }

    void DeployBomb()
    {
        Instantiate(bomb, transform.position, Quaternion.identity);
    }
}
