using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LevelManager : MonoBehaviour
{
    [Header("Settings: ")]
    [SerializeField] int lemmingAmount;
    [SerializeField] int lemmingsNeededToFinishLevel;

    [Header("References: ")]
    [SerializeField] GameObject gemParent;

    private List<GameObject> gems = new List<GameObject>();
    private int gemAmount;

    private void Start()
    {
        ApplyInitializationSettings();
    }

    private void ApplyInitializationSettings()
    {
        RefreshGemList();
        LemmingManager.Instance.InstantiateAllLemmings(lemmingAmount);
    }

    private void RefreshGemList()
    {
        foreach (Transform _gem in gemParent.transform)
        {
            gems.Add(_gem.gameObject);
        }
        gemAmount = gems.Count;

        UIManager.Instance.UpdateGemItem(0, gemAmount);
    }
}
