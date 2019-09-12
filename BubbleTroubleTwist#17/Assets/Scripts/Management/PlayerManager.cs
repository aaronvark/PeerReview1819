using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Bas.Interfaces;
using UnityEngine.UI;

public class PlayerManager : MonoBehaviour, IPlayer
{
    public List<PlayerData> playersStats;
    public GameObject player;
    public int lives = 3;
    public List<Image> livesImages;
    public bool testing = false;

    private void Start()
    {

        InitPlayers();

        InitLives();
    }

    public void InitPlayers()
    {
        InstantiatePlayers();

    }

    private void InitLives()
    {
        foreach (Image image in livesImages)
        {
            image.gameObject.SetActive(true);
        }
        EventManager.OnPlayerHitHandler += UpdatePlayerLives;
    }

    public void UpdatePlayerLives(int _amount)
    {
        /*
         * Nu werkt het zo:
         * De speler verliest telkens N aantal levels als de enemy ( bollen ) 
         * de speler raakt. Als de speler minder dan 1 leven over heeft na een
         * aanraking van de enemy dan verlies je het spel.
         * 
         * Wat ik wil:
         * Als de speler geraakt wordt door een bal, dan wordt het huidige level
         * waar de speler zich in bevindt herstart. Om dit te doen moet ik eerst 
         * mijn levelmanager aanpassen zodat deze de data van het huidige level 
         * waar de speler zich bevindt opslaat. 
         */
        lives -= _amount;
        //EventManager.OnSaveLevelHandler();
        if(lives < 1)
        {
            EventManager.OnGameOverHandler();
        }
        for (int attackTimes = 0; attackTimes <= _amount-1; attackTimes++)
        {
            if (lives > 0)
            {
                Image lastImage = livesImages?.FindLast(i => i.gameObject.activeSelf);
                if (lastImage) lastImage.gameObject.SetActive(false);
            }
        }

        EventManagerGen<float>.Broadcast(EVENT.reloadGame, LevelManager.Instance.LastPlayedLevel().currentCameraX);
    }

    public GameObject UpdateImage(Image _image)
    {
        return _image.gameObject;
    }

    private void InstantiatePlayers()
    {
        foreach(PlayerData playerStats in playersStats)
        {
            //Spawn a player foreach player in the PlayerData list
            GameObject playerGameObject = ObjectPooler.Instance.SpawnFromPool(player.name, playerStats.spawnVector, Quaternion.identity);
            //Get the interface of each player and give it its stats
            //var component = playerGameObject.GetComponent<IStats<PlayerData>>();
            //component.SetStats(playerStats);
            //playerGameObject.GetComponent<AbstractAvatarClass>().OnDataUpdateHandler(playerStats);
            playerGameObject.GetComponent<IStats<PlayerData>>().SetStats(playerStats);
            //EventManager.OnEntityDataHandler(playerStats);
            LevelManager.Instance.AddPlayer(playerGameObject);
        }
    }
}
