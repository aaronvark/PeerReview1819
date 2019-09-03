using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// To create a grid and safe the position of the blocks within the created grid
/// </summary>

public class Grid : MonoBehaviour
{
    public static Dictionary<Coordinate, Block> allBlocks = new Dictionary<Coordinate, Block>();

    [SerializeField] private int gridSizeX, gridSizeY;
    [SerializeField] private GameObject blockPrefab;

    private void Awake() {
        CreateGrid();
    }

    private void CreateGrid() {

        for (int x = 0; x < gridSizeX; x++) {
            for (int y = 0; y < gridSizeY; y++) {
                //DISCUSS

                //Saves the current coordinates for later use
                Coordinate _currentCoordinate = new Coordinate(x, y);

                //Creates a block, then adds the Block script to the object
                Block _newBlock = Instantiate(blockPrefab, new Vector3(x, y), Quaternion.identity, transform).AddComponent<Block>();

                //Initializes the newBlock with the coordinates
                _newBlock.Initialize(_currentCoordinate);
                

                allBlocks.Add(_currentCoordinate, _newBlock);
            }
        }
    }


}
