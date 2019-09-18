using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FloatingEntity : PoolObject
{
    [SerializeField]
    protected float speed = 5f;

    protected Rigidbody2D rb;

    public override void OnObjectReuse()
    {
        rb = GetComponent<Rigidbody2D>();
        //transform.Rotate(new Vector3(0, 0, Random.Range(0, 360)));
        rb.velocity = transform.right * speed;
        base.OnObjectReuse();
    }

    public override void Destroy()
    {
        base.Destroy();
    }
    public Rigidbody2D RigidBody
    {
        get { return rb; }
        set { rb = value; }
    }
}
