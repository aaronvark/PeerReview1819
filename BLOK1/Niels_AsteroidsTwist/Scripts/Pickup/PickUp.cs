using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PickUp : MonoBehaviour, IPickable
{
    private float metal;
    [SerializeField]
    private Vector2 MinMaxValue;

    public float PickItem()
    {
        gameObject.SetActive(false);
        return metal;
    }

    private void Start()
    {
        metal = Random.Range(MinMaxValue.x, MinMaxValue.y);
    }
}
