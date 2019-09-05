using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// Is a block in the grid
/// </summary>
public class Block : MonoBehaviour
{

    public bool isSet;
    public Coordinate coordinate;

    protected Renderer rend;

    public void Initialize(Coordinate _coordinate) {
        rend = GetComponent<Renderer>();
        coordinate = _coordinate;
    }

    public virtual void AssignColor(Color _color) {

        rend.material.SetColor("_BaseColor", _color);
    }

    public void SetBlock() {

        isSet = true;
    }
}
