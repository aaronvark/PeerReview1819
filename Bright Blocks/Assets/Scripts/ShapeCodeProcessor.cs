using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class ShapeCodeProcessor : MonoBehaviour {
    
    public static Coordinate[] IntToCoords(int _shapeCode, Coordinate _fromCoordinate) {

        //Calculates the number of blocks based on the position within the number
        //EXAMPLE the number 234 would be
        //up = 2
        //left = 3
        //right = 4

        int _up = _shapeCode/100;
        int _left = ((_shapeCode/100) % 1) * 10;
        int _right = ((_shapeCode / 10) % 1) * 10;


    }

}

