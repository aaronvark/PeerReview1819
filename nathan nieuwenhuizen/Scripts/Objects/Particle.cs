using System.Collections;
using System.Collections.Generic;
using UnityEngine;

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
