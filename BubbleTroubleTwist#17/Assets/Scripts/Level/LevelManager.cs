using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Bas.Interfaces;

public class LevelManager : MonoBehaviour, ILevel
{
    public List<Level> levels;

    public OnLevelUpdate onLevelUpdate;

    #region Singleton
    private static LevelManager instance;
    public static ILevel Instance
    {
        get
        {
            if (instance == null)
                instance = new LevelManager();
            return instance;
        }
    }

    private void Awake()
    {
        instance = this;
    }
    #endregion

    public void UpdateLevel()
    {
        Level level = levels?.Find(l => l.done.Equals(false));
        
        if(level != null)
        {
            if(CheckEnemiesAlive(level))
            {
                level.enemiesAlive--;
            }
            else
            {
                level.done = true;

            }
        }
        EventManager.Broadcast(EVENT.gameUpdateEvent);
        //Level nextLevel = levels?.FindLast(l => l.done.Equals(true));
    }

    private bool CheckEnemiesAlive(Level level)
    {
        return level.enemiesAlive > 1 ? true : false;
    }
}
