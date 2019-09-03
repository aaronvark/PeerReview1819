using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerInput : MonoBehaviour
{
    public MainBlockSpawner tempVar;

    private void Update() {
        if (Input.GetKeyDown(KeyCode.Space)) {
            tempVar.SpawnMainBlock();
        }
    }
}
