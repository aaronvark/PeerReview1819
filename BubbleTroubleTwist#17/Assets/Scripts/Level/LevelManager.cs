﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Bas.Interfaces;

public class LevelManager : MonoBehaviour, ILevel
{
    public List<Level> levels;

    public List<GameObject> players;

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

    private void Start()
    {
        //EventManager.AddHandler(EVENT.MyEvent2, UpdateLevel);
        EventManager.onUpdateCallerHandler += UpdateLevel;
    }

    public void UpdateLevel()
    {
        Level level = levels?.Find(l => l.done.Equals(false));
        
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
                    player.transform.position = level.nextLevelPosition.position;
                }
                EventManager.Broadcast(EVENT.gameUpdateEvent);
            }
        }
    }

    private void OnDestroy()
    {
        EventManager.onUpdateCallerHandler -= UpdateLevel;
    }

    public void AddPlayer(GameObject player)
    {
        players.Add(player);
    }

    private bool CheckEnemiesAlive(Level level)
    {
        return level.enemiesAlive > 1 ? true : false;
    }
}
