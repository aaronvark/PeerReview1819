using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class ShapeCodeProcessor : MonoBehaviour {
    
    //TODO int to string then to array
    public static List<Vector2> ShapeCodeToCoords(int[] _shapeCode, Vector2 _fromCoordinate) {

        if (_shapeCode.Length == 8) {

            List<Vector2> _coordList = new List<Vector2>();

            //Calculates the coords based of the position and size of the number within the shapecode

            _coordList.Add(new Vector2(_fromCoordinate.x, _fromCoordinate.y + _shapeCode[0]));                      //up
            _coordList.Add(new Vector2(_fromCoordinate.x + _shapeCode[1], _fromCoordinate.y));                      //right
            _coordList.Add(new Vector2(_fromCoordinate.x, _fromCoordinate.y - _shapeCode[2]));                      //down
            _coordList.Add(new Vector2(_fromCoordinate.x - _shapeCode[3], _fromCoordinate.y));                      //left

            _coordList.Add(new Vector2(_fromCoordinate.x - _shapeCode[4], _fromCoordinate.y + _shapeCode[4]));      //up-left
            _coordList.Add(new Vector2(_fromCoordinate.x + _shapeCode[5], _fromCoordinate.y + _shapeCode[5]));      //up-right
            _coordList.Add(new Vector2(_fromCoordinate.x + _shapeCode[6], _fromCoordinate.y - _shapeCode[6]));      //down-right
            _coordList.Add(new Vector2(_fromCoordinate.x - _shapeCode[7], _fromCoordinate.y - _shapeCode[7]));      //down-left



            return _coordList;
        } else {

            Debug.LogError("You have submitted a shapeCode which had the wrong size!");
            return null;
        }

    }

}

