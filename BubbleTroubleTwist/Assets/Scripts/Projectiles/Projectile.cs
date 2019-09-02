using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Projectile : MonoBehaviour
{
    public float speed;

    public Projectile(float _speed)
    {
        speed = _speed;
    }

    private void LateUpdate()
    {

    }
}
