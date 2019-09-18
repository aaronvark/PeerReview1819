using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ParticleManager : MonoBehaviour
{
	[SerializeField] private List<ParticleSystem> particles = new List<ParticleSystem>();
	private GameObject particleObject;

    private void Awake()
    {
		for (int i = 0; i < particles.Count; i++)
		{
			particles[i].Stop();
		}
    }

	public void PlayParticle(int indexNumber, Transform _transform)
	{
		for (int i = 0; i < particles.Count; i++)
		{
			Instantiate(particles[indexNumber], _transform.position, Quaternion.identity);
			particles[indexNumber].Play();
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