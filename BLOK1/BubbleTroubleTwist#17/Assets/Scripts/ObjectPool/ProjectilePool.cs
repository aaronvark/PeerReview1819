using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Bas.Interfaces;
using UnityEngine.SceneManagement;

public class ProjectilePool : GenericSingleton<ProjectilePool, IPooler>, IPooler
{
    public GameObject prefab;
    public int size;

    GenericObjectPooler<Projectile> objPool;

    private void Start()
    {
        objPool = new GenericObjectPooler<Projectile>(prefab, size);
    }

    public GameObject SpawnFromPool(Vector3 position, Quaternion rotation)
    {
        GameObject obj = objPool.GetNext().gameObject;
        obj.gameObject.SetActive(true);
        obj.transform.position = position;
        obj.transform.rotation = rotation;
        return obj;
    }

    public GameObject SpawnFromPool(GameObject tag, Vector3 position, Quaternion rotation)
    {
        throw new System.NotImplementedException();
    }

    public override void OnLevelFinishedLoading(Scene scene, LoadSceneMode mode)
    {
        base.OnLevelFinishedLoading(scene, mode);
        objPool = new GenericObjectPooler<Projectile>(prefab, size);
    }
}
