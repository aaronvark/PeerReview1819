using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Block : MonoBehaviour{

    public Block(Coordinate _coordinate) {

        coordinate = _coordinate;
    }

    public bool isSet;
    public Coordinate coordinate;

    private Material material;

    private void Awake() {
        material = GetComponent<Renderer>().material;
    }

    public void AssignColor(Color _color) {

        material.color = _color;
    }
}
