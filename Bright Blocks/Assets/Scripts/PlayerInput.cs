using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// Registrates and acts upon player input
/// </summary>
public class PlayerInput : MonoBehaviour
{
    public MainBlockManager tempVar;
    public MainBlock currentMainBlock;

    private void Update() {

        if (Input.GetKeyDown(KeyCode.Space)) {

            tempVar.SpawnShape();
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
