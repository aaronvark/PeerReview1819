using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Bas.Interfaces;
using System.IO;

public class LevelManager : GenericSingleton<LevelManager, ILevel>, ILevel
{
    public List<Level> levels;

    public List<GameObject> players;

    private string levelJsongString;

    private void Start()
    {
        FromJson();
        EventManager.OnLevelUpdateHandler += UpdateLevel;
        EventManagerGen<float>.AddHandler(EVENT.reloadGame, ResetLevel);
        EventManagerGen<float>.AddHandler(EVENT.reloadGame, SerializeToJson);
        EventManagerGen<float>.Broadcast(EVENT.reloadGame, LastPlayedLevel().currentCameraX);
    }

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

    private void OnDestroy()
    {
        EventManager.OnLevelUpdateHandler -= UpdateLevel;
    }

    private bool CheckEnemiesAlive(Level level)
    {
        return level.enemiesAlive > 1 ? true : false;
    }
}
