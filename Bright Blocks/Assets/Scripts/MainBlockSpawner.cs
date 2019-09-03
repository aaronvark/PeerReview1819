using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MainBlockSpawner : MonoBehaviour
{
    [SerializeField] private int spawnPositionX, spawnPositionY;
    private Coordinate spawnCoordinate;
    private Block startingBlock;

    private void Start() {

        spawnCoordinate = new Coordinate(spawnPositionX, spawnPositionY);
        startingBlock = Grid.allBlocks[spawnCoordinate];
    }

    public void SpawnMainBlock() {

        Shape _currentShape = ChooseRandomShape();
        MainBlock _currentMainBlock = startingBlock.gameObject.AddComponent<MainBlock>();

        //Temporary
        FindObjectOfType<PlayerInput>().currentMainBlock = _currentMainBlock;

        _currentMainBlock.AssignColor(_currentShape.color);
        _currentMainBlock.positionOfAttachedBlocks = _currentShape.shape;
    }

    private Shape ChooseRandomShape() {

        return MainBlockShapeList.shapeList[Random.Range(0, MainBlockShapeList.shapeList.Count-1)];
    }
}
