using UnityEngine;
using System;

/// <summary>
/// TargetPoints class, there are small white blocks in the game, when hit by the ball the targets give points and play particles.
/// </summary>
public class TargetPoints : MonoBehaviour
{
	public static Action<int, Transform> ParticleHitEvent;
	public static Action<int> TargetScoreEvent;

	[SerializeField] private int targetAmount = 80;
	[SerializeField] private ParticleEnum particleEnum; 

	private void OnTriggerEnter(Collider other)
	{
		if(other.gameObject.tag == Tags.Ball)
		{
			if(ParticleHitEvent != null)
			{
				ParticleHitEvent((int)particleEnum, transform);
			}
			if(TargetScoreEvent != null)
			{
				TargetScoreEvent(targetAmount);
			}
		}
	}
}