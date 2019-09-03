using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// Contains a list of the original tetris shapes embedded within float
/// </summary>
public class MainBlockShapeList : MonoBehaviour {

    public static List<Shape> shapeList = new List<Shape>();

    private void Start() {

        shapeList.Add(new Shape(300, Color.blue));

    }
}

