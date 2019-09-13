using System.Collections.Generic;
using UnityEngine;
using Bas.Interfaces;
using UnityEngine.UI;

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
    /// the amount of lives the players have ( lives are shared )
    /// </summary>
    public int lives = 3;

    /// <summary>
    /// list of images to visualize the lives
    /// </summary>
    public List<Image> livesImages;

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

        EventManager.AddHandler(EVENT.reloadGame, InitPlayers);

        //Initialize the players
        InitPlayers();
        
        //Initialize the lives
        InitLives();
    }

    /// <summary>
    /// public init/ call InstantiePlayers
    /// </summary>
    public void InitPlayers()
    {
        InstantiatePlayers();

    }

    /// <summary>
    /// Initialize lives
    /// </summary>
    private void InitLives()
    {
        foreach (Image image in livesImages)
        {
            image.gameObject.SetActive(true);
        }
        EventManager.OnPlayerHitHandler += UpdatePlayerLives;
    }

    /// <summary>
    /// Handles and updates the player lives
    /// </summary>
    /// <param name="_amount">amount of lives lost</param>
    public void UpdatePlayerLives(int _amount)
    {
        lives -= _amount;
        if(lives < 1)
        {
            EventManager.OnGameOverHandler();
        }
        for (int attackTimes = 0; attackTimes <= _amount-1; attackTimes++)
        {
            if (lives > 0)
            {
                Image lastImage = livesImages?.FindLast(i => i.gameObject.activeSelf);
                if (lastImage == null) return;
                if (lastImage) lastImage.gameObject.SetActive(false);
            }
        }
        EventManagerGen<float>.Broadcast(EVENT.reloadGame, LevelManager.Instance.LastPlayedLevel().currentCameraX);
    }

    /// <summary>
    /// Create players and give every player its data
    /// </summary>
    private void InstantiatePlayers()
    {
        foreach(PlayerData playerStats in playersStats)
        {
            //Spawn a player foreach player in the PlayerData list
            GameObject playerGameObject = ObjectPooler.Instance.SpawnFromPool(player.name, playerStats.spawnVector, Quaternion.identity);
            //Get the interface of each player and give it its stats
            playerGameObject.GetComponent<IStats<PlayerData>>().SetStats(playerStats);
            //Add the player in the levelManger
            LevelManager.Instance.AddPlayer(playerGameObject);
        }
    }
}
