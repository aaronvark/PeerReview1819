using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EntityExtraForce : Entity
{
    private void OnCollisionEnter2D(Collision2D collision)
    {
        Collider2D[] colliders = Physics2D.OverlapCircleAll(transform.position, 2f);

        Debug.Log(colliders.Length);
        foreach(Collider2D col in colliders)
        {
            Entity _tempEntity = col.transform.gameObject.GetComponent<Entity>();
            _tempEntity.DamageEntity(damage);

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
        Destroy(this.gameObject);
        
    }
}
