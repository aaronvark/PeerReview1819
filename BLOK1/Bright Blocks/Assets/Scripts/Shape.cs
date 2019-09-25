using UnityEngine;

public struct Shape
{

    public Shape(int[] _shapeOfShape, Color _colorOfShape)
    {

        color = _colorOfShape;
        shape = _shapeOfShape;
    }

    public Color color;
    public int[] shape;
}
