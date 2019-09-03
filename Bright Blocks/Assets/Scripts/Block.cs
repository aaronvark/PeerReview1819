using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Block : MonoBehaviour{

    public bool isSet;
    public Coordinate coordinate;

    private Material material;

    public void Initialize(int x, int y) {
        material = GetComponent<Renderer>().material;
    }

    public void AssignColor(Color _color) {

        material.color = _color;
    }
}
