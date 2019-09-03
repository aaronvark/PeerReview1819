using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Block : MonoBehaviour
{

    public bool isSet;
    public Coordinate coordinate;

    private Material material;

    public void Initialize(Coordinate _coordinate) {
        material = GetComponent<Renderer>().material;
        coordinate = _coordinate;
    }

    public virtual void AssignColor(Color _color) {

        material.color = _color;
    }
}
