using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public interface IUIManager
{
    void UpdateGemItem(int _index, int _amount);
    void UpdateGemItem(int _index, int _amount, int _maxAmount);

    void UpdateLemmingItem(int _amount);
    void UpdateLemmingItem(int _amount, int _maxAmount);
}

public class UIManager : MonoBehaviour, IUIManager
{
    private static IUIManager instance;
    public static IUIManager Instance
    {
        get
        {
            if (instance == null)
            {
                instance = new UIManager();
            }
            return instance;
        }
    }

    [SerializeField] UIItem lemming;
    [SerializeField] List<UIItem> gems;

    private void Awake()
    {
        instance = this;
    }

    private void Start()
    {
        InitializeItems();
    }

    private void InitializeItems()
    {
        UpdateLemmingItem(lemming.currentAmount);
        for (int i = 0; i < gems.Count; i++)
        {
            UpdateGemItem(i, gems[i].currentAmount);
        }
    }

    private void UpdateUIItem(UIItem _item)
    {
        _item.text.text = _item.currentAmount + "/" + _item.maxAmount;
        _item.image.sprite = _item.sprite;
    }

    public void UpdateGemItem(int _index, int _amount)
    {
        gems[_index].currentAmount += _amount;
        UpdateUIItem(gems[_index]);
    }
    public void UpdateGemItem(int _index, int _amount, int _maxAmount)
    {
        gems[_index].currentAmount += _amount;
        gems[_index].maxAmount = _maxAmount;
        UpdateUIItem(gems[_index]);
    }

    public void UpdateLemmingItem(int _amount)
    {
        lemming.currentAmount += _amount;
        UpdateUIItem(lemming);
    }
    public void UpdateLemmingItem(int _amount, int _maxAmount)
    {
        lemming.currentAmount += _amount;
        lemming.maxAmount = _maxAmount;
        UpdateUIItem(lemming);
    }
}

[System.Serializable]
public class UIItem
{
    public int maxAmount;
    public int currentAmount;
    public Sprite sprite;

    public Image image;
    public TextMeshProUGUI text;
}
