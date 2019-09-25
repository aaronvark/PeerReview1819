using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// Is a block in the grid
/// </summary>
public class Block : MonoBehaviour
{

    public bool isSet;
    public Vector2Int coordinate;

    private Renderer rend;
    private Color baseColor;

    public void Initialize(Vector2Int _coordinate)
    {
        rend = GetComponent<Renderer>();
        coordinate = _coordinate;
        baseColor = rend.material.color;
    }

    public void AssignColor(Color _color)
    {

        rend.material.SetColor("_BaseColor", _color);
    }

    public void SetBlock()
    {

        isSet = true;
    }

    public void ClearBlock()
    {

        isSet = false;
        AssignColor(baseColor);
    }

    public void TransferColorDown()
    {
        if (coordinate.y - 1 >= 0 && isSet && !Grid.allBlocks[new Vector2Int(coordinate.x, coordinate.y - 1)].isSet)
        {
            Grid.allBlocks[new Vector2Int(coordinate.x, coordinate.y - 1)].AssignColor(rend.material.GetColor("_BaseColor"));
            Grid.allBlocks[new Vector2Int(coordinate.x, coordinate.y - 1)].SetBlock();
            AssignColor(baseColor);
            isSet = false;
        }
    }

}
