using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Bas.Interfaces;

public class PlayerInput : MonoBehaviour, IStats<PlayerData>
{
    public PlayerData playerInput;

    public void SetStats(PlayerData data)
    {
        playerInput = data;
    }
}
