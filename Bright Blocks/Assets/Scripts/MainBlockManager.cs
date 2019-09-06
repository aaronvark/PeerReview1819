using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// Spawns the mainBlocks
/// </summary>
public class MainBlockManager : MonoBehaviour {

    [SerializeField] private Color baseColor;
    [SerializeField] private Vector2Int spawnCoordinate;
    private Vector2Int currentMainBlockCoordinate;
    private Shape currentShape;
    private List<Block> currentUsedBlocks = new List<Block>();

    private List<Shape> availableShapes = new List<Shape>();

    private void Start() {
        baseColor = Grid.allBlocks[spawnCoordinate].GetComponent<Renderer>().material.color;
        AddShapes();
    }

    private void AddShapes() {

        availableShapes.Add(new Shape(new int[] { 4, 0, 0, 0, 0, 0, 0, 0 }, Color.cyan));
        availableShapes.Add(new Shape(new int[] { 0, 2, 2, 0, 0, 0, 2, 0 }, Color.yellow));
        availableShapes.Add(new Shape(new int[] { 2, 2, 0, 2, 0, 0, 0, 0 }, Color.magenta));
        availableShapes.Add(new Shape(new int[] { 3, 2, 0, 0, 0, 0, 0, 0 }, Color.yellow));
        availableShapes.Add(new Shape(new int[] { 3, 0, 0, 2, 0, 0, 0, 0 }, Color.blue));
        availableShapes.Add(new Shape(new int[] { 0, 2, 2, 0, 0, 2, 0, 0 }, Color.red));
        availableShapes.Add(new Shape(new int[] { 2, 2, 0, 0, 0, 0, 2, 0 }, Color.green));
    }

    public void SpawnShape() {

        currentMainBlockCoordinate = spawnCoordinate;
        currentShape = ChooseRandomShape();
        Grid.allBlocks[spawnCoordinate].AssignColor(currentShape.color);
        UpdateCurrentUsedBlocks();
        ColorShapeAroundMainBlock(currentShape.color);
    }


    public void MoveShapeTowards(Direction _direction) {

        //Checks if shape can move towards specified direction
        if (Grid.CanShapeMove(_direction, ShapeCodeProcessor.ShapeCodeToCoords(currentShape.shape, currentMainBlockCoordinate))) {

            //Resets current shape to base color
            ColorShapeAroundMainBlock(baseColor);

            //Moves the main block down
            MoveMainBlockTowards(Grid.DirectionToCoords(_direction, currentMainBlockCoordinate));

            //Updates the list of blocks the shape is made of
            UpdateCurrentUsedBlocks();

            //Colors the shape around the main block
            ColorShapeAroundMainBlock(currentShape.color);
        }

    }

    public void SetShape() {

        Grid.allBlocks[currentMainBlockCoordinate].SetBlock();
        for (int i = 0; i < currentUsedBlocks.Count; i++) {

            currentUsedBlocks[i].SetBlock();
            //TODO Assign the shapecode within the blocks
        }

        SpawnShape();
    }

    private void UpdateCurrentUsedBlocks() {

        //Clears the list
        currentUsedBlocks.Clear();

        List<Vector2Int> _currentUsedBlocksCoordinates = ShapeCodeProcessor.ShapeCodeToCoords(currentShape.shape, currentMainBlockCoordinate);

        //Finds the blocks linked with the coordinates and adds them to the list
        for (int i = 0; i < _currentUsedBlocksCoordinates.Count; i++) {

            currentUsedBlocks.Add(Grid.allBlocks[_currentUsedBlocksCoordinates[i]]);
        }
    }

    private void ColorShapeAroundMainBlock(Color _color) {

        for (int i = 0; i < currentUsedBlocks.Count; i++) {

            currentUsedBlocks[i].AssignColor(_color);
        }
    }

    private void MoveMainBlockTowards(Vector2Int _coordinate) {

        //Resets current block to base color
        Grid.allBlocks[currentMainBlockCoordinate].AssignColor(baseColor);

        //Picks new block
        currentMainBlockCoordinate = _coordinate;

        //Colors the new block
        Grid.allBlocks[currentMainBlockCoordinate].AssignColor(currentShape.color);

    }

    private Shape ChooseRandomShape() {

        return availableShapes[Random.Range(0, availableShapes.Count)];
    }
}
