﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public delegate void RemovedLemmingEvent(GameObject _lemming);

public interface ILemmingManager
{
    void InstantiateAllLemmings(int _amount);
    void RemoveLemming(GameObject _lemming, bool _reachedExit = false);
}

public class LemmingManager : MonoBehaviour, ILemmingManager
{
    private static ILemmingManager instance;
    public static ILemmingManager Instance
    {
        get
        {
            if (instance == null)
            {
                instance = new LemmingManager();
            }
            return instance;
        }
    }

    [Header("Settings: ")]
    [SerializeField] int lemmingAmount;
    [SerializeField, Range(0.1f, 3f)] float delayPerLemming = 1;

    [Header("References: ")]
    [SerializeField] GameObject lemmingPrefab;
    [SerializeField] GameObject entrance;

    private List<GameObject> currentLemmingsInScene = new List<GameObject>();
    private Coroutine instantiateAllLemmingsRoutine;
    private int amountOfLemmingsThatHaveReachedTheExit;

    private void Awake()
    {
        instance = this;
    }

    private IEnumerator IEInstantiateAllLemmings()
    {
        UIManager.Instance.UpdateLemmingItem(amountOfLemmingsThatHaveReachedTheExit, lemmingAmount, currentLemmingsInScene.Count);

        for (int i = 0; i < lemmingAmount; i++)
        {
            SpawnLemming();
            yield return new WaitForSeconds(delayPerLemming);
        }

        Destroy(entrance);

        yield return null;
    }

    private void SpawnLemming()
    {
        GameObject _lemming = Instantiate(lemmingPrefab, entrance.transform.position, entrance.transform.rotation, transform);
        currentLemmingsInScene.Add(_lemming);
        UIManager.Instance.UpdateLemmingItem(amountOfLemmingsThatHaveReachedTheExit, lemmingAmount, currentLemmingsInScene.Count);
    }

    public void InstantiateAllLemmings(int _amount)
    {
        if (instantiateAllLemmingsRoutine != null) return;

        lemmingAmount = _amount;
        instantiateAllLemmingsRoutine = StartCoroutine(IEInstantiateAllLemmings());
    }

    public void RemoveLemming(GameObject _lemming, bool _reachedExit = false)
    {
        if (currentLemmingsInScene.Contains(_lemming))
            currentLemmingsInScene.Remove(_lemming);

        if (_reachedExit)
            amountOfLemmingsThatHaveReachedTheExit++;

        Destroy(_lemming);
        UIManager.Instance.UpdateLemmingItem(amountOfLemmingsThatHaveReachedTheExit, lemmingAmount, currentLemmingsInScene.Count);
    }
}
