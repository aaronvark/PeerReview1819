using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// Spawns the mainBlocks
/// </summary>
public class MainBlockManager : MonoBehaviour {

    [SerializeField] private Vector2 spawnCoordinate;
    private MainBlock mainBlock;

    public void SpawnMainBlock() {

        Shape _currentShape = ChooseRandomShape();
        mainBlock = Grid.allBlocks[spawnCoordinate].gameObject.AddComponent<MainBlock>();

        //Temporary
        FindObjectOfType<PlayerInput>().currentMainBlock = mainBlock;

        mainBlock.AssignColor(_currentShape.color);
        mainBlock.shapeCode = _currentShape.shape;
    }

    public void ResetMainBlock() {

        mainBlock.SetAttachedBlocks();

        Shape _currentShape = ChooseRandomShape();

        //Assigns color to the spawnVector2 block
        Grid.allBlocks[spawnCoordinate].AssignColor(_currentShape.color);
        Grid.allBlocks[spawnCoordinate].SetBlock();

        //Switches the mainblock with the spawnCoordinate block
        mainBlock.SwitchWithBlock(spawnCoordinate);

        //Gives the mainblock his new shape information
        mainBlock.AssignColor(_currentShape.color);
        mainBlock.shapeCode = _currentShape.shape;
        mainBlock.UpdateAttachedBlocks();
    }

    private Shape ChooseRandomShape() {

        return MainBlockShapeList.shapeList[Random.Range(0, MainBlockShapeList.shapeList.Count - 1)];
    }
}
