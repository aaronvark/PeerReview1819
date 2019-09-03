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

        //startingBlock.gameObject.AddComponent<MainBlock>().AssignColor();
    }
}
