using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent (typeof(ObjectPoolManager))]
public class GameManager : MonoBehaviour
{
    #region Singleton
    public static GameManager Instance;

    private void Awake()
    {
        Instance = this;
    }
    #endregion

    private Player playerObject;

    private void Start()
    {
        playerObject = Player.Instance;
    }

    private void StartGame() { }



}
