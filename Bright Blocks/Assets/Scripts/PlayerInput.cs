using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// Registrates and acts upon player input
/// </summary>
public class PlayerInput : MonoBehaviour
{
    public MainBlockManager tempVar;

    private void Update() {

        if (Input.GetKeyDown(KeyCode.Space)) {

            tempVar.SpawnShape();
        }

        if (Input.GetKeyDown(KeyCode.DownArrow)) {

            tempVar.MoveShapeTowards(Direction.Down);
        } else if (Input.GetKeyDown(KeyCode.LeftArrow)) {

            tempVar.MoveShapeTowards(Direction.Left);
        } else if (Input.GetKeyDown(KeyCode.RightArrow)) {

            tempVar.MoveShapeTowards(Direction.Right);
        }
    }
}
