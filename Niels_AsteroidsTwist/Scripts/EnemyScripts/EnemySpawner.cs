using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemySpawner : MonoBehaviour
{
    [SerializeField]
    private float spawnTime;
    private float timestamp = 0.0f;
    private Transform thePlayer;
    private ObjectPooler asteroidSpawnPool;
    const int LARGEASTEROIDSPAWNCHANCE = 20; // 20%

    private void Start()
    { 
        timestamp = Time.time + 0.0f;
        thePlayer = GameObject.Find("Player").transform;
        asteroidSpawnPool = GetComponent<ObjectPooler>();
    }

    private void Update()
    {
        if(GameManager.Instance.gameIsPlaying)
        {
            if (Time.time > timestamp && thePlayer != null)
            {
                // Gets random position on the edges of the screen
                float _randomX = Random.Range(-15, 15);
                float _randomY = Random.Range(-9, 9);
                if (_randomX > -8 && _randomX < 8)
                    return;

                Vector3 _spawnPoint = new Vector3(_randomX, _randomY, 0f);

                // Angles asteroid thorwards player
                Vector2 _direction = thePlayer.transform.position - _spawnPoint;
                float _angle = (Mathf.Atan2(_direction.y, _direction.x) * Mathf.Rad2Deg) - 90f;
                Quaternion _tempRot = Quaternion.AngleAxis(_angle, Vector3.forward);

                // Spawns large asteroid when the % is smaller than 20, if percentage is larger than 20 then spawn small asteroid
                int _randomInt = Random.Range(0, 100) < LARGEASTEROIDSPAWNCHANCE ? 1 : 0;
                //if(_randomInt < 20)
                //{
                //    _chosenInt = 1;
                //}
                //else
                //{
                //    _chosenInt = 0;
                //}

                asteroidSpawnPool.GetNext(_randomInt, _spawnPoint, _tempRot);
                timestamp = Time.time + spawnTime;
            }
        }
    }


}
