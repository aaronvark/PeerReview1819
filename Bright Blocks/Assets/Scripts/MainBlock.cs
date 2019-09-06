using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// Is the block in the middle of a tetris shape
/// </summary>
public class MainBlock : Block
{
    //ShapeCode which decides where the blocks around the mainblock are
    public int shapeCode;

    //Singleton
    public static MainBlock Instance {
        get { return instance; }
        private set { instance = value; }
    }
    private static MainBlock instance;
    

    private Color currentColorOfSelfAndAttachedBlocks;
    private Color baseColor;
    private List<Block> attachedBlocks = new List<Block>();

    


    private void Awake() {

        //Singleton script
        if (Instance == null) {

            Instance = this;
        }else if (Instance != this) {

            Destroy(gameObject);
        }

        DontDestroyOnLoad(gameObject);

        //Disables block script since it's no longer necessary
        GetComponent<Block>().enabled = false;

        Initialize(GetComponent<Block>().coordinate);
        baseColor = rend.material.color;
    }

    public override void AssignColor(Color _color) {

        base.AssignColor(_color);
        currentColorOfSelfAndAttachedBlocks = (_color != baseColor)? _color : currentColorOfSelfAndAttachedBlocks;
    }

    public void MoveTo(Direction _direction) {

        if (Grid.CanShapeMove(_direction, ShapeCodeProcessor.ShapeCodeToInt(shapeCode), coordinate)) {

            switch (_direction) {
                case Direction.Down:
                    SwitchWithBlock(new Vector2(coordinate.x, coordinate.y - 1));
                    break;
                case Direction.Left:
                    SwitchWithBlock(new Vector2(coordinate.x - 1, coordinate.y));
                    break;
                case Direction.Right:
                    SwitchWithBlock(new Vector2(coordinate.x + 1, coordinate.y));
                    break;
                default:
                    break;
            }

            ColorUpdate();
        } else {

            Debug.Log("Can't move there");
        }
    }

    public void SetAttachedBlocks() {

        for (int i = 0; i < attachedBlocks.Count; i++) {

            attachedBlocks[i].SetBlock();
        }
    }

    public void SwitchWithBlock(Vector2 _coordinate) {

        Vector3 _currentPosition = transform.position;

        Block _blockToMoveTo = Grid.allBlocks[_coordinate];

        //Update the dictionary on what is happening
        Grid.allBlocks.Remove(_coordinate);
        Grid.allBlocks.Remove(this.coordinate);

        Grid.allBlocks.Add(_coordinate, this);
        Grid.allBlocks.Add(this.coordinate, _blockToMoveTo);

        //Changes the Vector2s on the block
        _blockToMoveTo.coordinate = this.coordinate;
        coordinate = _coordinate;

        //Switches the blocks, making this block move down 1
        transform.position = _blockToMoveTo.transform.position;
        _blockToMoveTo.transform.position = _currentPosition;
    }

    //Should probably not be in this class
    public void UpdateAttachedBlocks() {

        attachedBlocks.Clear();
        List<Vector2> _Vector2s = Grid.IntToCoords(ShapeCodeProcessor.ShapeCodeToInt(shapeCode), coordinate);

        //Finds the blocks linked with the Vector2s
        for (int i = 0; i < _Vector2s.Count; i++) {

            if (Grid.allBlocks[_Vector2s[i]] != this) {

                attachedBlocks.Add(Grid.allBlocks[_Vector2s[i]]);
            }
        }
    }

    private void ColorUpdate() {

        //Setsback the old color of the attached blocks and then applies the color to the new blocks (basically making is look like the blocks have moved)
        ColorAttachedBlocks(baseColor);
        UpdateAttachedBlocks();
        ColorAttachedBlocks(currentColorOfSelfAndAttachedBlocks);
    }

    private void ColorAttachedBlocks(Color _color) {

        for (int i = 0; i < attachedBlocks.Count; i++) {

            attachedBlocks[i].AssignColor(_color);
        }
    }
}
