using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Actor : MonoBehaviour {

    public GameObject bomb;
    public bool bombDeployCheck = true;

    // Update is called once per frame
    private void Update() {
        if (Input.GetKey(KeyCode.E) && bombDeployCheck) {
            bombDeployCheck = false;
            DeployBomb();
        }
    }

    private void DeployBomb() {
        Instantiate(bomb, transform.position, Quaternion.identity);
    }
}
