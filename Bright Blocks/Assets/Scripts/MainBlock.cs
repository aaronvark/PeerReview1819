using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MainBlock : Block
{
    public int positionOfAttachedBlocks;

    private Color currentColorOfSelfAndAttachedBlocks;
    private Color baseColor;
    private List<Block> attachedBlocks = new List<Block>();

    private void Awake() {

        //Disables block script since it's no longer necessary
        GetComponent<Block>().enabled = false;

        Initialize(GetComponent<Block>().coordinate);
        baseColor = rend.material.color;
    }

    public override void AssignColor(Color _color) {

        base.AssignColor(_color);
        currentColorOfSelfAndAttachedBlocks = _color;
    }

    public void MoveTo(Direction _direction) {
        
        switch (_direction) {
            case Direction.Down:
                SwitchWithBlock(new Coordinate(coordinate.xCoordinate, coordinate.yCoordinate-1));
                break;
            case Direction.Left:
                break;
            case Direction.Right:
                break;
            default:
                break;
        }
    }

    private void SwitchWithBlock(Coordinate _coordinate) {

        Vector3 _currentPosition = transform.position;

        Block _blockToMoveTo = Grid.allBlocks[_coordinate];

        //Update the dictionary on what is happening
        Grid.allBlocks.Remove(_coordinate);
        Grid.allBlocks.Remove(this.coordinate);

        Grid.allBlocks.Add(_coordinate, this);
        Grid.allBlocks.Add(this.coordinate, _blockToMoveTo);

        //Changes the coordinates on the block
        _blockToMoveTo.coordinate = this.coordinate;
        coordinate = _coordinate;

        //Switches the blocks, making this block move down 1
        transform.position = _blockToMoveTo.transform.position;
        _blockToMoveTo.transform.position = _currentPosition;

        //This moves the rest of the shape with the mainblock
        ColorAttachedBlocks();
    }

    //Should probably not be in this class
    private void UpdateAttachedBlocks() {

        List<Coordinate> _coordinates = ShapeCodeProcessor.IntToCoords(positionOfAttachedBlocks, coordinate);

        //Finds the blocks linked with the coordinates
        for (int i = 0; i < _coordinates.Count; i++) {

            if (Grid.allBlocks.ContainsKey(_coordinates[i])) {

                attachedBlocks.Add(Grid.allBlocks[_coordinates[i]]);
            }
        }
    }

    private void ColorAttachedBlocks() {

        UpdateAttachedBlocks();

        for (int i = 0; i < attachedBlocks.Count; i++) {

            attachedBlocks[i].AssignColor(currentColorOfSelfAndAttachedBlocks);
        }
    }
}
