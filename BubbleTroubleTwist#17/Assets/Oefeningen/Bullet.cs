using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Bas.Interfaces;

public class Bullet : MonoBehaviour
{
    public float shootSpeed;
    // Update is called once per frame
    void Update()
    {
        transform.Translate(transform.forward * shootSpeed * Time.deltaTime);
    }

    private void OnCollisionEnter(Collision collision)
    {
        var space = collision.gameObject.GetComponent<IDamagable<int>>();
        if (space != null)
        {
            space.TakeDamage(10);
        }
    }
}
