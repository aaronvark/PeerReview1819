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

    public static bool CanShapeMove(Direction _direction, int[] _shapeCodeArray, Coordinate _fromCoordinate) {

        List<Coordinate> _potentialCoordinates = new List<Coordinate>();

        switch (_direction) {
            case Direction.Down:
                _potentialCoordinates = IntToCoords(_shapeCodeArray, new Coordinate(_fromCoordinate.xCoordinate, _fromCoordinate.yCoordinate - 1));
                break;
            case Direction.Left:
                _potentialCoordinates = IntToCoords(_shapeCodeArray, new Coordinate(_fromCoordinate.xCoordinate - 1, _fromCoordinate.yCoordinate));
                break;
            case Direction.Right:
                _potentialCoordinates = IntToCoords(_shapeCodeArray, new Coordinate(_fromCoordinate.xCoordinate + 1, _fromCoordinate.yCoordinate));
                break;
            default:
                break;
        }

        //Checks if coordinates exist within the grid and are not already occupied by colored blocks
        for (int i = 0; i < _potentialCoordinates.Count; i++) {

            if (!allBlocks.ContainsKey(_potentialCoordinates[i]) || allBlocks[_potentialCoordinates[i]].isSet) {

                //Checks if the block has reached the bottom or a colored block
                if(_potentialCoordinates[i].yCoordinate < 0 || (_direction == Direction.Down && allBlocks[_potentialCoordinates[i]].isSet)) {

                    FindObjectOfType<MainBlockManager>().ResetMainBlock();
                    return false;
                }
                return false;
            }
        }

        return true;
    }

    public static List<Coordinate> IntToCoords(int[] _shapeCodeArray, Coordinate _fromCoordinate) {

        List<Coordinate> _coordinates = new List<Coordinate>();

        for (int i = 0; i < _shapeCodeArray[0]+1; i++) {
            _coordinates.Add(new Coordinate(_fromCoordinate.xCoordinate, _fromCoordinate.yCoordinate + i));
        }

        for (int i = 0; i < _shapeCodeArray[1]+1; i++) {
            _coordinates.Add(new Coordinate(_fromCoordinate.xCoordinate - i, _fromCoordinate.yCoordinate));
        }

        for (int i = 0; i < _shapeCodeArray[2]+1; i++) {
            _coordinates.Add(new Coordinate(_fromCoordinate.xCoordinate + i, _fromCoordinate.yCoordinate));
        }

        return _coordinates;
    }

    private void CreateGrid() {

        for (int y = 0; y < gridSizeY; y++) {
            for (int x = 0; x < gridSizeX; x++) {
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
