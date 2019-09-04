using UnityEngine;

public class PaddleDirectionMath : MonoBehaviour
{
	/// <summary>
	// Idea:
	// 1  -0.5  0  0.5   1  <- x value
	// ===================  <- paddle
	//
	/// </summary>
	public float hitFactor(Vector3 _ballPos, Vector3 _paddlePos, float _paddleWidth){		
		return (_ballPos.x - _paddlePos.x) / _paddleWidth;
	}
}