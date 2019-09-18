using System.Collections.Generic;
using UnityEngine;
using Bas.Interfaces;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

/// <summary>
/// Manages and initializes the players
/// </summary>
public class PlayerManager : MonoBehaviour, IPlayer
{
    /// <summary>
    /// list of player data, here we make and give the players its using stats
    /// </summary>
    public List<PlayerData> playersStats;

    /// <summary>
    /// Placeholder player gameobject 
    /// </summary>
    public GameObject player;

    /// <summary>
    /// bool for testing purpose only
    /// </summary>
    public bool testing = false;


    private void Start()
    {
        //Initialize the players
        
        //InitPlayers();
    }

    /// <summary>
    /// Create players and give every player its data
    /// </summary>
    private void InstantiatePlayers()
    {
        foreach (PlayerData playerStats in playersStats)
        {
            //Spawn a player foreach player in the PlayerData list
            //GameObject playerGameObject = ObjectPooler.Instance.SpawnFromPool(player.name, playerStats.spawnVector, Quaternion.identity);
            GameObject playerGameObject = ObjectPoolerLearning.Instance.SpawnFromPool<PlayerMovement>(playerStats.spawnVector, Quaternion.identity).gameObject;
            //Get the interface of each player and give it its stats
            playerGameObject.GetComponent<IStats<PlayerData>>().SetStats(playerStats);
            //Add the player in the levelManger                       
            LevelManager.Instance.AddPlayer(playerGameObject, playersStats);
        }
    }

    /// <summary>
    /// public init/ call InstantiePlayers
    /// </summary>
    public void InitPlayers()
    {
        InstantiatePlayers();
    }

    public virtual void OnLevelFinishedLoading(Scene scene, LoadSceneMode mode)
    {
        //Initialize the players
        if(scene.buildIndex == 1)
            InitPlayers();
    }

    private void OnEnable()
    {
        SceneManager.sceneLoaded += OnLevelFinishedLoading;
    }

    private void OnDisable()
    {
        SceneManager.sceneLoaded -= OnLevelFinishedLoading;
    }
}
