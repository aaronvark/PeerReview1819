using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class ShapeCodeProcessor : MonoBehaviour {
    
    public static List<Coordinate> IntToCoords(int _shapeCode, Coordinate _fromCoordinate) {

        List<Coordinate> _coordinates = new List<Coordinate>();

        //Calculates the number of blocks based on the position within the number
        //EXAMPLE the number 234 would be
        //up = 2
        //left = 3
        //right = 4

        int _up = (int)((float)_shapeCode/100);
        int _left = Mathf.RoundToInt((((float)_shapeCode /100) % 1) * 10);
        int _right = Mathf.RoundToInt((((float)_shapeCode / 10) % 1) * 10);

        Debug.Log(_up);
        Debug.Log(_left);
        Debug.Log(_right);

        for (int i = 0; i < _up; i++) {
            _coordinates.Add(new Coordinate(_fromCoordinate.xCoordinate, _fromCoordinate.yCoordinate + i));
        }

        for (int i = 0; i < _left; i++) {
            _coordinates.Add(new Coordinate(_fromCoordinate.xCoordinate - i, _fromCoordinate.yCoordinate));
        }

        for (int i = 0; i < _left; i++) {
            _coordinates.Add(new Coordinate(_fromCoordinate.xCoordinate + i, _fromCoordinate.yCoordinate));
        }

        return _coordinates;

    }

}

