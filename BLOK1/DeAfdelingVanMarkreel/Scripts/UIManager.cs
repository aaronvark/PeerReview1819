using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public interface IUIManager
{
    void UpdateGemItem(int _amount, int _maxAmount);
    void UpdateLemmingItem(int _amount, int _maxAmount, int _amountInScene);
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
    [SerializeField] UIItem gem;

    private void Awake()
    {
        instance = this;
    }

    //private void Start()
    //{
    //    InitializeItems();
    //}

    //private void InitializeItems()
    //{
    //    UpdateLemmingItem(lemming.currentAmount);
    //    for (int i = 0; i < gems.Count; i++)
    //    {
    //        UpdateGemItem(i, gems[i].currentAmount);
    //    }
    //}

    private void UpdateUIItem(UIItem _item, string _text)
    {
        _item.text.text = _text;
        _item.image.sprite = _item.sprite;
    }

    public void UpdateGemItem(int _amount, int _maxAmount)
    {
        string _text = _amount + "/" + _maxAmount;
        UpdateUIItem(gem, _text);
    }

    public void UpdateLemmingItem(int _amount, int _maxAmount, int _amountInScene)
    {
        lemming.text.text = "OUT: " + _amountInScene + "    IN: " +  (100f / _maxAmount * _amount) + "%";
        lemming.image.sprite = lemming.sprite;
    }
}

[System.Serializable]
public class UIItem
{
    public Sprite sprite;
    public Image image;
    public TextMeshProUGUI text;
}
