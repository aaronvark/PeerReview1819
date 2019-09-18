using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public class TargetPoints : MonoBehaviour
{
	public static Action<int> TargetScoreEvent;
	[SerializeField] private int targetAmount = 80;

	private void OnTriggerEnter(Collider other)
	{
		if(other.gameObject.tag == Tags.Ball)
		{
			Debug.Log("TargetPoints Hit");
			if(TargetScoreEvent != null)
			{
				TargetScoreEvent(targetAmount);
			}
		}
	}
}