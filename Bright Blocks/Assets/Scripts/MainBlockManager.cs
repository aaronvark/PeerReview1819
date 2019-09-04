using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// Spawns the mainBlocks
/// </summary>
public class MainBlockManager : MonoBehaviour {

    [SerializeField] private int spawnPositionX, spawnPositionY;
    private Coordinate spawnCoordinate;
    private MainBlock mainBlock;

    private void Start() {

        spawnCoordinate = new Coordinate(spawnPositionX, spawnPositionY);
    }

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

        //Assigns color to the spawncoordinate block
        Grid.allBlocks[spawnCoordinate].AssignColor(_currentShape.color);
        Grid.allBlocks[spawnCoordinate].SetBlock();

        //Switches the mainblock with the spawncoordinate block
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
