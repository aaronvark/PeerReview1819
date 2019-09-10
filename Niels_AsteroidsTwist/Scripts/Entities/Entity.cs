using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Entity : MonoBehaviour
{
    public float GetHealth
    {
        get { return health; }
    }
    public float damage;
    [SerializeField]
    protected float health;
    [SerializeField]
    protected GameObject[] destroyInstObjects;
    [SerializeField]
    protected EntityType typeOfEntity;

    protected EntityType lastCollidedType;

    public void DamageEntity(float damagePoints)
    {
        health -= damagePoints;
        if(health <= 0)
        {
            Destroy(this.gameObject);
        }
    }
    // Detects colission with objects
    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.transform.gameObject)
        { 
            Entity _tempEntity = collision.transform.gameObject.GetComponent<Entity>();
            //      _tempEntity?.health = health;
            if (_tempEntity != null)
            {
                lastCollidedType = _tempEntity.typeOfEntity;
                DamageEntity(_tempEntity.damage);
            }
        }
    }

    // Spawns particles on destruction and adds score if object is asteroid
    private void OnDisable()
    {
        if (health <= 0)
        {
            foreach (GameObject _intObject in destroyInstObjects)
            {
                Instantiate(_intObject, transform.position, transform.rotation);
            }

            if (lastCollidedType != EntityType.Asteroid && lastCollidedType != EntityType.LargeAsteroid)
            {
                switch (typeOfEntity)
                {
                    case EntityType.Asteroid:
                        ScoreManager.Instance.addPoint(1f);
                        break;
                    case EntityType.LargeAsteroid:
                        ScoreManager.Instance.addPoint(2f);
                        break;
                    default:
                        break;
                }
            }
        }
    }
}


public enum EntityType
{
    Asteroid,
    LargeAsteroid,
    Projectile,
    Player
}
