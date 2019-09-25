using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// ParticleManager class, Instantiates particles through enums.
/// </summary>
public class ParticleManager : MonoBehaviour
{
	[SerializeField] private List<ParticleSystem> particles = new List<ParticleSystem>();

	public void PlayParticle(int _indexNumber, Transform _transform)
	{
		for (int i = 0; i < particles.Count; i++)
		{
			Instantiate(particles[_indexNumber], _transform.position, Quaternion.identity);
			particles[_indexNumber].Play();
		}
	}

	private void Awake()
    {
		for (int i = 0; i < particles.Count; i++)
		{
			particles[i].Stop();
		}
    }

	private void OnEnable()
	{
		Bouncer.ParticlePlayEvent += PlayParticle;
		TargetPoints.ParticleHitEvent += PlayParticle;
	}

	private void OnDisable()
	{
		Bouncer.ParticlePlayEvent -= PlayParticle;
		TargetPoints.ParticleHitEvent -= PlayParticle;
	}
}