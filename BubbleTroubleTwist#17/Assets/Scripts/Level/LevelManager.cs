using System.Collections.Generic;
using UnityEngine;
using Bas.Interfaces;
using System.IO;
using UnityEngine.SceneManagement;

/// <summary>
/// Level management class that handles all level related behaviour
/// **Singleton** we may only have one instance at a time
/// </summary>
public class LevelManager : GenericSingleton<LevelManager, ILevel>, ILevel
{
    /// <summary>
    /// list of all the levels we have
    /// </summary>
    public List<Level> levels = new List<Level>();

    /// <summary>
    /// list of all players in the game
    /// </summary>
    public List<GameObject> players = new List<GameObject>();

    /// <summary>
    /// string to store the levelJson data in
    /// </summary>
    private string levelJsongString;

    private void OnEnable()
    {
        SceneManager.sceneLoaded += OnLevelFinishedLoading;

        //subscribe UpdateLevel to OnLevelUpdateHandler stored in EventManager so we can easy call the update later on
        EventManager.OnLevelUpdateHandler += UpdateLevel;
        EventManager.AddHandler(EVENT.initializeGame, ResetPlayerPositions);
        EventManager.AddHandler(EVENT.initializeGame, ResetLevels);
        EventManager.AddHandler(EVENT.initializeGame, FromJson);
        EventManagerGen<int>.AddHandler(EVENT.selectGame, SelectLevel);
        EventManagerGen<float>.AddHandler(EVENT.gameUpdateEvent, SerializeToJson);
    }

    public override void Awake()
    {
        base.Awake();
        DontDestroyOnLoad(this.gameObject);
    }

    private void Start()
    {
        /*
       //When the game starts we want to load the json data
       FromJson();
       //EventManager.AddHandler(EVENT.initializeGame, ResetPlayerPositions);
       EventManagerGen<float>.AddHandler(EVENT.gameUpdateEvent, SerializeToJson);
       EventManagerGen<float>.Broadcast(EVENT.reloadGame, LastPlayedLevel().currentCameraX);
       //EventManager.Broadcast(EVENT.initializeGame);

       //TO DO:

        * BUG analysis: als ik op de reset game knop druk dan wordt het event OnGameOver gecalled 
        * die ervoor zorgt dat de scene opnieuw opstart. Op het moment dat de scene opnieuw is geladen
        * krijg ik meerdere nullreferenties die alleemaal erop neer komen dat bepaalde instanties niet meer 
        * te bereiken zijn. De objecten referen naar een oude "versie" van de instanties en omdat het level
        * herladen is zijn er nieuwe instanties. Deze instanties worden niet geupdate bij de objecten. Dit zorgt
        * voor een nullreferentie. 
        * 
        * Misschien kan het zijn dat omdat ik mijn scene herlaad en ik bepaalde events in mijn EventManager 
        * nog niet un-subscribe deze conflicten en oude referenties blijven zoeken. 
        * 
        * Oplossingen:
        * --Kijken of ik de objecten kan laten updaten zodat deze de nieuwe versie van de instanties krijgen.
        * --Kijken of als ik ipv het herladen van dezelfde scene eerst terug kan gaan naar het menu en dan 
        * opnieuw het spel in kan laden het probleem zich nog voordoet. 
        * --De resetGame veranderen naar dat ipv de scene opnieuw wordt geladen alleen alle data die gereset 
        * zou moeten worden resetten. Dit zorgt ervoor dat er geen nieuwere versie van de instanties gemaakt 
        * kan worden. 
        */
    }

    public override void OnLevelFinishedLoading(Scene scene, LoadSceneMode mode)
    {
        base.OnLevelFinishedLoading(scene, mode);
        //EventManager.AddHandler(EVENT.initializeGame, FromJson);
        FromJson();
        
        EventManagerGen<float>.Broadcast(EVENT.reloadGame, LastPlayedLevel().currentCameraX);
        EventManager.Broadcast(EVENT.initializeGame);
    }

    /*(OPTIONAL)
     * Idee: Misschien is het leuk en handig om voor mijn levels een state machine
     * te bouwen die checkt: OnLevelEntered() OnLevelUpdated() OnLevelExited()
     * dit zorgt ervoor dat ik meer overzicht krijg over waar de speler zich bevindt 
     * en over hoe ik de levels kan managen.
     */

    /// <summary>
    /// Updates the current level state
    /// </summary>
    public void UpdateLevel()
    {
        Level level = LastPlayedLevel();
        if(level != null)
        {
            if(CheckEnemiesAlive(level))
            {
                level.enemiesAlive--;
                if(level.enemiesAlive < 1)
                {
                    NextLevel(level);
                }
                return;
            }
            else
            {
                NextLevel(level);
            }
        }
    }

    public void SelectLevel(int levelIndex)
    {
        if (levels.Count < 1) return;
        levels[levelIndex].done = false;
        for (int i = levelIndex; i < levels.Count; i++)
        {
            levels[i].done = false;
        }
        SerializeToJson(0);
        StartCoroutine(LoadSceneAsyncInBackground());
    }

    public System.Collections.IEnumerator LoadSceneAsyncInBackground()
    {
        AsyncOperation asyncLoad = SceneManager.LoadSceneAsync(1);

        // Wait until the asynchronous scene fully loads
        while (!asyncLoad.isDone)
        {
            yield return null;
        }
    }
    
    private void NextLevel(Level level)
    {
        level.done = true;
        if (players == null) return;
        foreach (GameObject player in players)
        {
            if (player == null) continue;
            if (player != null) player.transform.position = level.nextLevelPosition;
        }
        EventManagerGen<float>.Broadcast(EVENT.gameUpdateEvent, LastPlayedLevel().currentCameraX);
        EventManager.Broadcast(EVENT.initializeGame);
    }

    /// <summary>
    /// Resets the game when clicked ( use with UI button )
    /// </summary>
    public void ResetGameOnClick()
    {
        foreach(Level level in levels)
        {
            level.enemiesAlive = level.enemyAmounts;
            level.done = false;           
        }
        EventManager.OnGameOverHandler();
    }

    /// <summary>
    /// Resets the levelss
    /// </summary>
    public void ResetLevels()
    {
        foreach(Level level in levels)
        {
            level.enemiesAlive = level.enemyAmounts;
        }
    }
    /// <summary>
    /// Reads the Json file if found. And gives the levels list all the level data. 
    /// </summary>
    public void FromJson()
    {
        if (File.Exists(Application.dataPath + "/LevelsData.json"))
        {
            levelJsongString = File.ReadAllText(Application.dataPath + "/LevelsData.json");
        }
        if (levelJsongString == null || levelJsongString == string.Empty)
        {
            return;
        }
        levels = JsonHelper.FromJsonList<Level>(levelJsongString);
    }

    /// <summary>
    /// Serializes the levels list to a json string 
    /// </summary>
    /// <param name="x"></param>
    public void SerializeToJson(float x)
    {
        if (levels == null || levels.Count < 1) return;
        //Convert to Json
        levelJsongString = JsonHelper.ToJsonList(levels);
        Debug.Log(levelJsongString);
        File.WriteAllText(Application.dataPath + "/LevelsData.json", levelJsongString);
    }

    /// <summary>
    /// Reset the player positions
    /// </summary>
    /// <param name="x"></param>
    public void ResetPlayerPositions()
    {
        Level _level = LastPlayedLevel();
        foreach (GameObject player in players)
        {
            if (player == null) return;
            player.transform.position = _level.currentLevelPostion;
        }
    }


    /// <summary>
    /// Search the last played level in levels list
    /// </summary>
    /// <returns></returns>
    public Level LastPlayedLevel()
    {
        return levels?.Find(l => l.done.Equals(false));
    }

    /// <summary>
    /// Returns the current levels
    /// </summary>
    /// <returns></returns>
    public List<Level> GiveLevels()
    {
        return levels;
    }

    /// <summary>
    /// Clears the players list 
    /// </summary>
    public void ClearPlayers()
    {
        players.Clear();
    }

    /// <summary>
    /// Add a player to the player list
    /// </summary>
    /// <param name="player"></param>
    public void AddPlayer(GameObject player, List<PlayerData> playersData)
    {
        foreach(GameObject _player in players)
        {
            if (_player == null)
            {
                players.Remove(_player);
            }
        }
        //if (players.Count > playersData.Count) players.Clear();
        //else players.Add(player);
        players.Add(player);
    }

    /// <summary>
    /// Create a new level
    /// </summary>
    /// <param name="_prefab"></param>
    /// <param name="_position"></param>
    public void CreateLevel(GameObject _prefab, Vector3 _position)
    {
        GameObject newLevel = GameObject.Instantiate(_prefab, _position, _prefab.transform.rotation);
    }

    /// <summary>
    /// When this instance gets destroyed we unsubscribe 
    /// </summary>
    private void OnDisable()
    {
        EventManager.OnLevelUpdateHandler -= UpdateLevel;
        SceneManager.sceneLoaded -= OnLevelFinishedLoading;

        //EventManagerGen<float>.RemoveHandler(EVENT.reloadGame);
    }

    /// <summary>
    /// Check if there are enemies alive in the current level
    /// </summary>
    /// <param name="level"></param>
    /// <returns></returns>
    private bool CheckEnemiesAlive(Level level)
    {
        return level.enemiesAlive > 0 ? true : false;
    }
}
