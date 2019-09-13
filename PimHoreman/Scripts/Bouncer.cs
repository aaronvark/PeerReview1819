using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public class Bouncer : MonoBehaviour
{
	public static Action BouncerHitEvent;

	[SerializeField] private Ball ball;
	[SerializeField] private int bumperForce = 500;

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
		}
	}
}