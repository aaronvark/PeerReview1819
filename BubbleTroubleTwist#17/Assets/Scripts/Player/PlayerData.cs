using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class PlayerData : StatsBase<PlayerData>
{
    public string id;
    public string horizontalAxis;
    public string verticalAxis;
    public KeyCode fireKey;
    public int level;
    public Vector3 spawnVector;   
}
