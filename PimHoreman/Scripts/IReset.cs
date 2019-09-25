using UnityEngine;

/// <summary>
/// Interface IReset, Has a Vector3 to reset the position of an object and one function.
/// </summary>
public interface IReset 
{
	Vector3 ResetPosition { get; set; }

	void ResetObject();
}