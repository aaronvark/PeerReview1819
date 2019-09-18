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
	public static Action<int, Transform> ParticlePlayEvent;
	public static Action BouncerHitEvent;
	public static Action<int> BouncerScoreEvent;

	[Tooltip("Type of Ball:")]
	[SerializeField] private Ball ball;

	[Tooltip("Amount of Force:")]
	[SerializeField] private int bumperForce = 10;

	[Tooltip("Points Type:")]
	[SerializeField] private BouncerTypes bouncerTypes;

	[SerializeField] private ParticleEnum particleEnum;


	private void BouncerHit()
    {
		ball.ApplyForce(transform.position, bumperForce, ForceMode.VelocityChange);
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
			if(ParticlePlayEvent != null)
			{
				ParticlePlayEvent((int)particleEnum, transform);
			}
			if(BouncerScoreEvent != null)
			{
				BouncerScoreEvent((int)bouncerTypes);
			}
		}
	}
}