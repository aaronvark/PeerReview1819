using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Bas.Interfaces;

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
            //Spawn a player foreach player in the PlayerData list
            GameObject playerGameObject = ObjectPooler.Instance.SpawnFromPool(player.name, playerStats.spawnVector, Quaternion.identity);
            //Get the interface of each player and give it its stats
            var component = playerGameObject.GetComponent<IStats<PlayerData>>();
            component.SetStats(playerStats);
            LevelManager.Instance.AddPlayer(playerGameObject);
        }
    }
}
