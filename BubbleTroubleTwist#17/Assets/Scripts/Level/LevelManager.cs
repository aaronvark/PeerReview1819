using System.Collections.Generic;
using UnityEngine;
using Bas.Interfaces;
using System.IO;

/// <summary>
/// Level management class that handles all level related behaviour
/// **Singleton** we may only have one instance at a time
/// </summary>
public class LevelManager : GenericSingleton<LevelManager, ILevel>, ILevel
{
    /// <summary>
    /// list of all the levels we have
    /// </summary>
    public List<Level> levels;

    /// <summary>
    /// list of all players in the game
    /// </summary>
    public List<GameObject> players;

    /// <summary>
    /// string to store the levelJson data in
    /// </summary>
    private string levelJsongString;

    private void Awake()
    {
        //**WORK IN PROGRESS**//
        DontDestroyOnLoad(this);
    }

    private void Start()
    {
        //When the game starts we want to load the json data
        FromJson();

        //subscribe UpdateLevel to OnLevelUpdateHandler stored in EventManager so we can easy call the update later on
        EventManager.OnLevelUpdateHandler += UpdateLevel;
        
        //TO DO:
        /*
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
        EventManager.Broadcast(EVENT.reloadGame);
        EventManagerGen<float>.AddHandler(EVENT.reloadGame, ResetLevel);
        EventManagerGen<float>.AddHandler(EVENT.reloadGame, SerializeToJson);
        EventManagerGen<float>.Broadcast(EVENT.reloadGame, LastPlayedLevel().currentCameraX);
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
                return;
            }
            else
            {
                level.done = true;
                if (players == null) return;
                foreach(GameObject player in players)
                {
                    player.transform.position = level.nextLevelPosition;
                }
                EventManagerGen<float>.Broadcast(EVENT.gameUpdateEvent, LastPlayedLevel().currentCameraX);
            }
        }
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
        ResetLevel(0);
        EventManagerGen<float>.Broadcast(EVENT.reloadGame, 0);
        EventManager.OnGameOverHandler();
    }

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

    public void SerializeToJson(float x)
    {
        if (levels == null || levels.Count < 1) return;
        //Convert to Json
        levelJsongString = JsonHelper.ToJsonList(levels);
        Debug.Log(levelJsongString);
        File.WriteAllText(Application.dataPath + "/LevelsData.json", levelJsongString);
    }

    public void ResetLevel(float x)
    {
        Level _level = LastPlayedLevel();
        foreach (GameObject player in players)
        {
            if (player == null) return;
            player.transform.position = _level.currentLevelPostion;
        }
    }

    public Level LastPlayedLevel()
    {
        return levels?.Find(l => l.done.Equals(false));
    }

    public List<Level> GiveLevels()
    {
        return levels;
    }

    public void AddPlayer(GameObject player)
    {
        players.Add(player);
    }

    public void CreateLevel(GameObject _prefab, Vector3 _position)
    {
        GameObject newLevel = GameObject.Instantiate(_prefab, _position, _prefab.transform.rotation);
    }

    private void OnDestroy()
    {
        EventManager.OnLevelUpdateHandler -= UpdateLevel;
    }

    private bool CheckEnemiesAlive(Level level)
    {
        return level.enemiesAlive > 0 ? true : false;
    }
}
