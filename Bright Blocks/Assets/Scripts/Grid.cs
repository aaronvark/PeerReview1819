using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Grid : MonoBehaviour
{
    [SerializeField] private int gridSizeX, gridSizeY;
    [SerializeField] private GameObject blockPrefab;

    private Dictionary<Block, Coordinate> allBlocks;

    private void Start() {
        CreateGrid();
    }

    private void CreateGrid() {

        for (int x = 0; x < gridSizeX; x++) {
            for (int y = 0; y < gridSizeY; y++) {
                Instantiate(blockPrefab, new Vector3(x, y), Quaternion.identity, transform).AddComponent<Block>();
            }
        }
    }
}
