using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class DestructionBlock : Block
{
    [SerializeField]
    private int damage;

    private void Start()
    {
        base.Start();
    }

    private void Update()
    {
        base.Update();
    }

    public override void OnCollisionEnter(Collision _coll)
    {
        base.OnCollisionEnter(_coll);

        IDamagable _damagable = _coll.gameObject.GetComponent<IDamagable>();

        if (_damagable != null)
        {
            _damagable.GetDamage(damage);
            Pool.Instance.ReturnObjectToPool<Block>(this);
        }
    }

    private void OnBecameInvisible()
    {
        base.OnBecameInvisible();
    }
}
