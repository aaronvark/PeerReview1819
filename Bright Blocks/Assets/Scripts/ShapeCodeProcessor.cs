using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class ShapeCodeProcessor : MonoBehaviour {
    
    public static int[] ShapeCodeToInt(int _shapeCode) {

        //Calculates the number of blocks based on the position within the number
        //EXAMPLE the number 234 would be
        //up = 2
        //left = 3
        //right = 4

        int _up = (int)((float)_shapeCode/100);
        int _left = Mathf.RoundToInt((((float)_shapeCode /100) % 1) * 10);
        int _right = Mathf.RoundToInt((((float)_shapeCode / 10) % 1) * 10);

        int[] _shapeCodeArray = { _up, _left, _right };

        return _shapeCodeArray;

    }

}

