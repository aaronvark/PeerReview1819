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
        } else if (Input.GetKeyDown(KeyCode.LeftArrow)) {

            currentMainBlock.MoveTo(Direction.Left);
        } else if (Input.GetKeyDown(KeyCode.RightArrow)) {

            currentMainBlock.MoveTo(Direction.Right);
        }
    }
}
