using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ResetZone : MonoBehaviour
{
	private void OnTriggerEnter(Collider collision)
	{
		if(collision.gameObject.tag == Tags.Ball)
		{
			IReset iReset = (IReset)collision.gameObject.GetComponent(typeof(IReset));

			if(iReset != null)
			{
				iReset.ResetBall();
			}
		}
	}
}