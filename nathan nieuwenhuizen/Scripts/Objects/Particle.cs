using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// A particle class that pulls itself back after the particle has played.
/// </summary>
public class Particle : PoolObject
{
    private ParticleSystem ps;
    // Start is called before the first frame update
    void Start()
    {
        ps = GetComponent<ParticleSystem>();
    }
    IEnumerator WaitToDestroy()
    {
        while (!ps.isStopped)
        {
            yield return new WaitForFixedUpdate();
        }
        base.Destroy();
    }

    public override void OnObjectReuse()
    {
        ps = GetComponent<ParticleSystem>();

        ps.Play();
        base.OnObjectReuse();
        StartCoroutine(WaitToDestroy());
    }
}
