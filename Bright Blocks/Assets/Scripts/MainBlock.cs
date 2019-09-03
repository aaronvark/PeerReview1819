using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MainBlock : Block
{
    public Color currentColorOfSelfAndAttachedBlocks;

    private int positionOfAttachedBlocks;
    private Block[] attachedBlocks;

    public override void AssignColor(Color _color) {
        base.AssignColor(_color);
        currentColorOfSelfAndAttachedBlocks = _color;
    }
}
