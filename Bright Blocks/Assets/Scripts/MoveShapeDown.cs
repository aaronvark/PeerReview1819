using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// Moves the shapes down on a timer
/// </summary>
public class MoveShapeDown : MonoBehaviour
{
    public float autoMoveDownTimer;
    
    IEnumerator MoveDownTimer() {

        yield return new WaitForSeconds(autoMoveDownTimer);
    }

}
