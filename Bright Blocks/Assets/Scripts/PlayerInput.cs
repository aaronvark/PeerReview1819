using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerInput : MonoBehaviour
{
    public MainBlockSpawner tempVar;
    public MainBlock currentMainBlock;

    private void Update() {

        if (Input.GetKeyDown(KeyCode.Space)) {

            tempVar.SpawnMainBlock();
        }

        if (Input.GetKeyDown(KeyCode.DownArrow)) {

            currentMainBlock.MoveTo(Direction.Down);
        }
    }
}
