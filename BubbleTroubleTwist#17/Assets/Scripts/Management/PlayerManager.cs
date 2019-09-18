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
    [SerializeField] private List<PlayerData> playersStats;
    public List<PlayerData> PlayersStats { get => playersStats; set => playersStats = value; }

    /// <summary>
    /// Placeholder player gameobject 
    /// </summary>
    [SerializeField] private GameObject player;
    public GameObject Player { get => player; set => player = value; }

    private void OnEnable()
    {
        SceneManager.sceneLoaded += OnLevelFinishedLoading;
    }

    private void OnDisable()
    {
        SceneManager.sceneLoaded -= OnLevelFinishedLoading;
    }

    /// <summary>
    /// Create players and give every player its data
    /// </summary>
    private void InstantiatePlayers()
    {
        //We clear the list of players stored in the level manager
        LevelManager.Instance.ClearPlayers();
        foreach (PlayerData playerStats in PlayersStats)
        {
            //Spawn a player foreach player in the PlayerData list
            GameObject playerGameObject = ObjectPoolerLearning.Instance.SpawnFromPool<PlayerMovement>(LevelManager.Instance.LastPlayedLevel().currentLevelPostion, Quaternion.identity).gameObject;
            //Get the interface of each player and give it its stats
            playerGameObject.GetComponent<IStats<PlayerData>>().SetStats(playerStats);
            //Add the player in the levelManger                       
            LevelManager.Instance.AddPlayer(playerGameObject, PlayersStats);
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
}
