using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// Spawns the mainBlocks
/// </summary>
public class MainBlockManager : MonoBehaviour {

    [SerializeField] private Vector2 spawnCoordinate { get; }
    private Vector2 currentMainBlockCoordinate;
    private Shape currentShape;

    private Color baseColor;

    private List<Shape> availableShapes = new List<Shape>();

    private void AddShapes() {

        availableShapes.Add(new Shape(new int[] {1, 1, 1, 1, 1, 1, 1, 1 }, Color.blue));
    }

    public void SpawnShape() {

        currentMainBlockCoordinate = spawnCoordinate;
        currentShape = ChooseRandomShape();
        Grid.allBlocks[spawnCoordinate].AssignColor(currentShape.color);
        ColorShapeAroundMainBlock(currentShape.color);
    }

    //Moves shape down by 1
    public void MoveShapeDown() {

        //Resets current shape to base color
        ColorShapeAroundMainBlock(baseColor);

        //Moves the main block down
        MoveMainBlockDown();

        //Colors the shape around the main block
        ColorShapeAroundMainBlock(currentShape.color);

    }

    //Colors the shape around the mian block
    private void ColorShapeAroundMainBlock(Color _color) {

        //TODO Check if coords are viable

        List<Vector2> _coordsOfShape = ShapeCodeProcessor.ShapeCodeToCoords(currentShape.shape, currentMainBlockCoordinate);

        for (int i = 0; i < _coordsOfShape.Count; i++) {

            Grid.allBlocks[_coordsOfShape[i]].AssignColor(_color);
        }
    }

    private void MoveMainBlockDown() {

        //Resets current block to base color
        Grid.allBlocks[currentMainBlockCoordinate].AssignColor(baseColor);

        //Picks new block
        currentMainBlockCoordinate = new Vector2(currentMainBlockCoordinate.x, currentMainBlockCoordinate.y - 1);

        //Colors the new block
        Grid.allBlocks[currentMainBlockCoordinate].AssignColor(currentShape.color);

    }

    private Shape ChooseRandomShape() {

        return availableShapes[Random.Range(0, MainBlockShapeList.shapeList.Count - 1)];
    }
}
