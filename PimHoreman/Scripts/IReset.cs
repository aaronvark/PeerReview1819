using UnityEngine;

public interface IReset 
{
	Vector3 ResetPosition { get; set; }

	void ResetObject();
}