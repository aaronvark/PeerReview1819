using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Bas.Interfaces;

[System.Serializable]
public class PlayerData 
{
    public string id;
    public string horizontalAxis;
    public string verticalAxis;
    public int level;
    public Vector3 spawnVector;
}

public class PlayerManager : MonoBehaviour, IPlayer
{
    public List<PlayerData> playersStats;
    public GameObject player;
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.E))
            InitPlayers();
    }

    public void InitPlayers()
    {
        InstantiatePlayers();
    }

    public void InstantiatePlayers()
    {
        foreach(PlayerData playerStats in playersStats)
        {
            //GameObject playerGameObject = ObjectPooler.Instance.SpawnFromPool(playerStats.id, playerStats.spawnVector, Quaternion.identity);
            //playerGameObject.GetComponent<Player>().entityDataHandler += SetPlayerData;
            GameObject playerGameObject = Instantiate(player, playerStats.spawnVector, Quaternion.identity);
            playerGameObject.GetComponent<PlayerMovement>().entityStats = playerStats;
        }
    }

    private void SetPlayerData(PlayerData data)
    {

    }

    /*
    #region Singleton
    private static PlayerManager instance;
    public static PlayerManager Instance
    {
        get
        {
            if (instance == null)
                instance = new PlayerManager();
            return instance;
        }
    }
    #endregion
    */
}
