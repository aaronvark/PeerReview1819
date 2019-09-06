using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// To create a grid and safe the position of the blocks within the created grid
/// </summary>

public class Grid : MonoBehaviour
{
    public static Dictionary<Vector2, Block> allBlocks = new Dictionary<Vector2, Block>();

    [SerializeField] private Vector2 gridSize;
    [SerializeField] private GameObject blockPrefab;

    private void Awake() {
        CreateGrid();
    }

    public static bool CanShapeMove(Direction _direction, int[] _shapeCodeArray, Vector2 _fromCoordinate) {

        List<Vector2> _potentialCoordinate = new List<Vector2>();

        switch (_direction) {
            case Direction.Down:
                _potentialCoordinate = IntToCoords(_shapeCodeArray, new Vector2(_fromCoordinate.x, _fromCoordinate.y - 1));
                break;
            case Direction.Left:
                _potentialCoordinate = IntToCoords(_shapeCodeArray, new Vector2(_fromCoordinate.x - 1, _fromCoordinate.y));
                break;
            case Direction.Right:
                _potentialCoordinate = IntToCoords(_shapeCodeArray, new Vector2(_fromCoordinate.x + 1, _fromCoordinate.y));
                break;
            default:
                break;
        }

        //Checks if Vector2s exist within the grid and are not already occupied by colored blocks
        for (int i = 0; i < _potentialCoordinate.Count; i++) {

            if (!allBlocks.ContainsKey(_potentialCoordinate[i]) || allBlocks[_potentialCoordinate[i]].isSet) {

                //Checks if the block has reached the bottom or a colored block
                if(_potentialCoordinate[i].y < 0 || (_direction == Direction.Down && allBlocks[_potentialCoordinate[i]].isSet)) {

                    FindObjectOfType<MainBlockManager>().ResetMainBlock();
                    return false;
                }
                return false;
            }
        }

        return true;
    }

    public static List<Vector2> IntToCoords(int[] _shapeCodeArray, Vector2 _fromCoordinate) {

        List<Vector2> _coordinates = new List<Vector2>();

        for (int i = 0; i < _shapeCodeArray[0]+1; i++) {
            _coordinates.Add(new Vector2(_fromCoordinate.x, _fromCoordinate.y + i));
        }

        for (int i = 0; i < _shapeCodeArray[1]+1; i++) {
            _coordinates.Add(new Vector2(_fromCoordinate.x - i, _fromCoordinate.y));
        }

        for (int i = 0; i < _shapeCodeArray[2]+1; i++) {
            _coordinates.Add(new Vector2(_fromCoordinate.x + i, _fromCoordinate.y));
        }

        return _coordinates;
    }

    private void CreateGrid() {

        for (int y = 0; y < gridSize.y; y++) {
            for (int x = 0; x < gridSize.x; x++) {
                //DISCUSS

                //Saves the current Vector2s for later use
                Vector2 _currentCoordinate = new Vector2(x, y);

                //Creates a block, then adds the Block script to the object
                Block _newBlock = Instantiate(blockPrefab, new Vector3(x, y), Quaternion.identity, transform).AddComponent<Block>();

                //Initializes the newBlock with the Vector2s
                _newBlock.Initialize(_currentCoordinate);
                

                allBlocks.Add(_currentCoordinate, _newBlock);
            }
        }
    }

}
