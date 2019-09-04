using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Entity : MonoBehaviour
{
    [SerializeField]
    private float health;
    public float damage;
    [SerializeField]
    private GameObject destroyParticleObj;
    [SerializeField]
    private EntityType typeOfEntity;

    private EntityType lastCollidedType;

    private void DamageEntity(float damagePoints) {
        health -= damagePoints;
        if(health <= 0) {
            Destroy(this.gameObject);
        }
    }
    // Detects colission with objects
    private void OnCollisionEnter2D(Collision2D collision) {
        if (collision.transform.gameObject) { 
            Entity _tempEntity = collision.transform.gameObject.GetComponent<Entity>();
            lastCollidedType = _tempEntity.typeOfEntity;
            DamageEntity(_tempEntity.damage);
        }
    }

    // Spawns particles on destruction and adds score if object is asteroid
    private void OnDestroy() {
        Instantiate(destroyParticleObj, transform.position, transform.rotation);
        if (typeOfEntity == EntityType.Asteroid && lastCollidedType != EntityType.Asteroid) {
            ScoreManager.Instance.addPoint();
        }
    }
}


public enum EntityType
{
    Asteroid,
    Projectile,
    Player
}
