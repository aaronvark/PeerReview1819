using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public enum BouncerTypes
{
	LowPoints = 25,
	HighPoints = 50
}

public class Bouncer : MonoBehaviour
{
	public static Action BouncerHitEvent;
	public static Action<int> BouncerScoreEvent;

	[SerializeField] private Ball ball;
	[SerializeField] private int bumperForce = 100;
	[SerializeField] private BouncerTypes bouncerTypes;

	private void BouncerHit()
    {
		ball.ExplosiveForceBall(bumperForce, transform.position, 1);

		if (BouncerHitEvent != null)
		{
			BouncerHitEvent();
		}
    }

	private void OnCollisionEnter(Collision collision)
	{
		if(collision.gameObject.tag == Tags.Ball)
		{
			BouncerHit();
			if(BouncerScoreEvent != null)
			{
				BouncerScoreEvent((int)bouncerTypes);
			}
		}
	}
}