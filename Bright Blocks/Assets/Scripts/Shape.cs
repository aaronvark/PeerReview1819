using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public struct Shape {

    public Shape(float _shapeOfShape, Color _colorOfShape) {

        color = _colorOfShape;
        shape = _shapeOfShape;
    }

    public Color color;
    public float shape;
}
