using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public interface IGemManager
{
    void RemoveGem(GameObject _gem);
}

public class GemManager : MonoBehaviour, IGemManager
{
    private static IGemManager instance;
    public static IGemManager Instance
    {
        get
        {
            if (instance == null)
            {
                instance = new GemManager();
            }
            return instance;
        }
    }

    private List<GameObject> currentGemsInScene = new List<GameObject>();
    private int amountOfGemsCollected = 0;
    private int maxAmountOfGems = 0;

    private void Awake()
    {
        instance = this;
    }

    private void Start()
    {     
        FillGemList();
    }

    private void FillGemList()
    {
        foreach (Transform _child in transform)
        {
            currentGemsInScene.Add(_child.gameObject);
        }

        maxAmountOfGems = currentGemsInScene.Count;
        UIManager.Instance.UpdateGemItem(amountOfGemsCollected, maxAmountOfGems);
    }

    public void RemoveGem(GameObject _gem)
    {
        if (currentGemsInScene.Contains(_gem))
            currentGemsInScene.Remove(_gem);

        Destroy(_gem);
        amountOfGemsCollected++;
        UIManager.Instance.UpdateGemItem(amountOfGemsCollected, maxAmountOfGems);
    }
}
