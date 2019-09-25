using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EntityExtraForce : Entity
{
    protected ObjectPooler shockWavePool;

    protected override void Awake()
    {
        base.Awake();
        shockWavePool = GameObject.Find("ShockWavePool").GetComponent<ObjectPooler>();
    }

    protected override void OnDeath()
    {
        base.OnDeath();
        shockWavePool.GetNext(0, transform.position, transform.rotation);
    }


    private void OnCollisionEnter2D(Collision2D collision)
    {
        Collider2D[] colliders = Physics2D.OverlapCircleAll(transform.position, 2f);

        Debug.Log(colliders.Length);
        foreach(Collider2D col in colliders)
        {
            Entity _tempEntity = col.transform.gameObject.GetComponent<Entity>();
            if (_tempEntity != null)
            {
                _tempEntity.lastCollidedType = typeOfEntity;
                _tempEntity?.DamageEntity(damage);
            }

            Rigidbody2D rb = col.GetComponent<Rigidbody2D>();
            if(rb != null && rb.transform.name != "Player")
            {
                Vector2 _Vector = new Vector2();
                _Vector = (col.transform.position - transform.position);
                _Vector = _Vector * 5000f;
                Debug.Log(_Vector);
                rb.AddRelativeForce(_Vector);
            }
        }
        CameraShake.OnShake?.Invoke(.1f, 5f, .01f);
        gameObject.SetActive(false);
        
    }
}
