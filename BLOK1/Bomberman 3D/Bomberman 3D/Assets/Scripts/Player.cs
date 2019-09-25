using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : Actor
{
    private static Player _instance;

    public static Player Instance
    {
        get
        {
            return _instance;
        }
        set
        {
            _instance = value;
        }
    }

    private void Start()
    {
        _instance = this;
    }

    public override void Die()
    {
        base.Die();
        Debug.Log("Player Dies");
        GameManager.Instance.Reload();
    }
}
