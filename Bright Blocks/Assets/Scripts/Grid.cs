using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// To create a grid and safe the position of the blocks within the created grid
/// </summary>

public class Grid : MonoBehaviour
{
    public static Dictionary<Vector2Int, Block> allBlocks = new Dictionary<Vector2Int, Block>();

    [SerializeField] private Vector2Int gridSize;
    [SerializeField] private GameObject blockPrefab;

    private void Awake() {
        CreateGrid();
    }

    public static bool CanShapeMove(List<Vector2Int> _fromCoordinates, Direction _direction) {

        List<Vector2Int> _potentialCoordinates = new List<Vector2Int>();

        //Adds all potential coordinates to a list
        for (int i = 0; i < _fromCoordinates.Count; i++) {

            _potentialCoordinates.Add(DirectionToCoords(_direction, _fromCoordinates[i]));
        }

        

        return AreTheseCoordinatesAvailable(_potentialCoordinates, _direction);
    }


    public static bool AreTheseCoordinatesAvailable(List<Vector2Int> _coordinates, Direction _direction = Direction.Left) {

        //Checks if the potential coordinates exist within the grid and are not already occupied by colored blocks
        for (int i = 0; i < _coordinates.Count; i++) {

            if (!allBlocks.ContainsKey(_coordinates[i]) || allBlocks[_coordinates[i]].isSet) {

                //Checks if the block has reached the bottom or a colored block
                if (_coordinates[i].y < 0 || (_direction == Direction.Down && allBlocks[_coordinates[i]].isSet)) {

                    FindObjectOfType<MainBlockManager>().SetShape();
                    return false;
                }
                return false;
            }
        }

        return true;
    }

    public static Vector2Int DirectionToCoords(Direction _direction, Vector2Int _coordinate) {

        Vector2Int _newCoordinate = Vector2Int.zero;

        switch (_direction) {
            case Direction.Down:
                _newCoordinate = new Vector2Int(_coordinate.x, _coordinate.y - 1);
                break;
            case Direction.Left:
                _newCoordinate = new Vector2Int(_coordinate.x - 1, _coordinate.y);
                break;
            case Direction.Right:
                _newCoordinate = new Vector2Int(_coordinate.x + 1, _coordinate.y);
                break;
            default:
                break;
        }

        return _newCoordinate;
    }

    

    private void CreateGrid() {

        for (int y = 0; y < gridSize.y; y++) {
            for (int x = 0; x < gridSize.x; x++) {
                //DISCUSS

                //Saves the current Vector2Ints for later use
                Vector2Int _currentCoordinate = new Vector2Int(x, y);

                //Creates a block, then adds the Block script to the object
                Block _newBlock = Instantiate(blockPrefab, new Vector3(x, y), Quaternion.identity, transform).AddComponent<Block>();

                //Initializes the newBlock with the Vector2Ints
                _newBlock.Initialize(_currentCoordinate);
                

                allBlocks.Add(_currentCoordinate, _newBlock);
            }
        }
    }

}
