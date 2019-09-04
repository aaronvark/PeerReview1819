using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// Spawns the mainBlocks
/// </summary>
public class MainBlockManager : MonoBehaviour {

    [SerializeField] private int spawnPositionX, spawnPositionY;
    private Coordinate spawnCoordinate;
    private Block startingBlock;
    private MainBlock mainBlock;

    private void Start() {

        spawnCoordinate = new Coordinate(spawnPositionX, spawnPositionY);
        startingBlock = Grid.allBlocks[spawnCoordinate];
    }

    public void SpawnMainBlock() {

        Shape _currentShape = ChooseRandomShape();
        mainBlock = startingBlock.gameObject.AddComponent<MainBlock>();

        //Temporary
        FindObjectOfType<PlayerInput>().currentMainBlock = mainBlock;

        mainBlock.AssignColor(_currentShape.color);
        mainBlock.shapeCode = _currentShape.shape;
    }

    public void ResetMainBlock() {

        mainBlock.SetAttachedBlocks();

        Shape _currentShape = ChooseRandomShape();

        mainBlock.AssignColor(_currentShape.color);
        mainBlock.shapeCode = _currentShape.shape;
        mainBlock.SwitchWithBlock(spawnCoordinate);
        mainBlock.UpdateAttachedBlocks();
    }

    private Shape ChooseRandomShape() {

        return MainBlockShapeList.shapeList[Random.Range(0, MainBlockShapeList.shapeList.Count - 1)];
    }
}
