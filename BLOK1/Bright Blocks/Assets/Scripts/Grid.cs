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

    [SerializeField] private Demonstration_MeshProcessing meshOutliner;

    private void Awake()
    {
        CreateGrid();
    }

    public bool CanShapeMove(List<Vector2Int> _fromCoordinates, Direction _direction)
    {

        List<Vector2Int> _potentialCoordinates = new List<Vector2Int>();

        //Adds all potential coordinates to a list
        for (int i = 0; i < _fromCoordinates.Count; i++)
        {

            _potentialCoordinates.Add(DirectionToCoords(_direction, _fromCoordinates[i]));
        }



        return AreTheseCoordinatesAvailable(_potentialCoordinates, _direction);
    }

    public bool AreTheseCoordinatesAvailable(List<Vector2Int> _coordinates, Direction _direction = Direction.Left)
    {

        //Checks if the potential coordinates exist within the grid and are not already occupied by colored blocks
        for (int i = 0; i < _coordinates.Count; i++)
        {

            if (!allBlocks.ContainsKey(_coordinates[i]) || allBlocks[_coordinates[i]].isSet)
            {

                //Checks if the block has reached the bottom or a colored block
                if (_coordinates[i].y < 0 || (_direction == Direction.Down && allBlocks[_coordinates[i]].isSet))
                {

                    //Delegate potential
                    FindObjectOfType<MainBlockManager>().SetShape();
                    List<Block> _madeBlocks = GetAllMadeLines();
                    if (_madeBlocks.Count >= gridSize.x)
                    {
                        ClearBlocks(GetAllMadeLines());
                        MoveAllUsedBlocksDown(_madeBlocks.Count / gridSize.x, GetLowestYBlock(_madeBlocks));

                    }
                    return false;
                }
                return false;
            }
        }

        return true;
    }

    public Vector2Int DirectionToCoords(Direction _direction, Vector2Int _coordinate)
    {

        Vector2Int _newCoordinate = Vector2Int.zero;

        switch (_direction)
        {
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



    private void CreateGrid()
    {

        for (int y = 0; y < gridSize.y; y++)
        {
            for (int x = 0; x < gridSize.x; x++)
            {

                //Saves the current coordinates for later use
                Vector2Int _currentCoordinate = new Vector2Int(x, y);

                //Creates a block, then adds the Block script to the object
                Block _newBlock = Instantiate(blockPrefab, new Vector3(x, y), Quaternion.identity, transform).AddComponent<Block>();

                //Initializes the newBlock with the Vector2Ints
                _newBlock.Initialize(_currentCoordinate);


                allBlocks.Add(_currentCoordinate, _newBlock);

                meshOutliner.OutlineMesh(_newBlock.gameObject);
            }
        }
    }

    private void ClearBlocks(List<Block> _blocks)
    {

        for (int i = 0; i < _blocks.Count; i++)
        {

            _blocks[i].ClearBlock();
        }
    }

    private void MoveAllUsedBlocksDown(int _linesToMove, int _lowestYCoordinate)
    {
        for (int i = 0; i < _linesToMove; i++)
        {
            for (int y = _lowestYCoordinate; y < gridSize.y; y++)
            {
                for (int x = 0; x < gridSize.x; x++)
                {

                    allBlocks[new Vector2Int(x, y)].TransferColorDown();
                }
            }
        }

    }

    //TODO only check the lines a shape has interacted with
    //Check if all the blocks in a horizontal lines have the isSet bool as true
    public List<Block> GetAllMadeLines()
    {

        List<Block> _allBlocksInMadeLines = new List<Block>();

        for (int y = 0; y < gridSize.y; y++)
        {
            if (CheckIfBlocksAreSet(GetBlocksAtY(y)))
            {
                _allBlocksInMadeLines.AddRange(GetBlocksAtY(y));
            }
        }

        return _allBlocksInMadeLines;

    }

    private bool CheckIfBlocksAreSet(List<Block> _blockList)
    {

        for (int i = 0; i < _blockList.Count; i++)
        {
            if (!_blockList[i].isSet)
            {

                return false;
            }
        }

        return true;
    }

    private List<Block> GetBlocksAtY(int _y)
    {

        List<Block> _blocks = new List<Block>();

        for (int x = 0; x < gridSize.x; x++)
        {
            _blocks.Add(allBlocks[new Vector2Int(x, _y)]);
        }

        return _blocks;
    }

    private int GetLowestYBlock(List<Block> _blockList)
    {
        int _lowestY = gridSize.y + 2;

        for (int i = 0; i < _blockList.Count; i++)
        {
            if (_lowestY > _blockList[i].coordinate.y)
            {

                _lowestY = _blockList[i].coordinate.y;
            }
        }

        return _lowestY;
    }

}
