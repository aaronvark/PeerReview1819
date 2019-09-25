using UnityEngine;
using System;

/// <summary>
/// Enum to select scores for the different bumpers
/// </summary>
public enum BouncerTypes
{
	LowPoints = 25,
	HighPoints = 50
}

/// <summary>
/// Bouncer Class, When the ball hits the bouncer it will give points, play particles and shoots the ball away by applying force.
/// </summary>
public class Bouncer : MonoBehaviour
{
	public static Action<int, Transform> ParticlePlayEvent;
	public static Action BouncerScreenshakeEvent;
	public static Action<int> BouncerScoreEvent;

	[Tooltip("Type of Ball:")]
	[SerializeField] private Ball ball;

	[Tooltip("Amount of Force:")]
	[SerializeField] private int bumperForce = 10;

	[Header("Enums:")]
	[SerializeField] private BouncerTypes bouncerTypes;
	[SerializeField] private ParticleEnum particleEnum;

	/// <summary>
	/// BouncerHit applies force and fires an Action.
	/// </summary>
	private void BouncerHit()
    {
		ball.ApplyForce(transform.position, bumperForce, ForceMode.VelocityChange);
		if (BouncerScreenshakeEvent != null)
		{
			BouncerScreenshakeEvent();
		}
    }

	/// <summary>
	/// OnCollisionEnter there will be two Actions that will be fired, one for points and the other for instaniating particles.
	/// </summary>
	/// <param name="collision"></param>
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