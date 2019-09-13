using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public enum EVENT { gameUpdateEvent, reloadGame }; // ... Other events
public static class EventManager
{
    //Static delegates
    public delegate void OnUpdateCaller();
    public static OnUpdateCaller OnLevelUpdateHandler { get; set; }

    public delegate void OnEnemyHit();
    public static OnEnemyHit OnEnemyHitHandler { get; set; }

    public delegate void EntityDataHandler(PlayerData stats);
    public static EntityDataHandler OnEntityDataHandler { get; set; }

    public delegate void OnScoreChanged(int _amount);
    public static OnScoreChanged OnScoreChangedHandler { get; set; }

    public delegate void OnGameOver();
    public static OnGameOver OnGameOverHandler { get; set; }

    public delegate void OnPlayerHit(int damage);
    public static OnPlayerHit OnPlayerHitHandler { get; set; }

    public delegate void OnSaveLevel();
    public static OnSaveLevel OnSaveLevelHandler { get; set; }


    #region Handling events using System.Action()
    // Stores the delegates that get called when an event is fired
    private static Dictionary<EVENT, Action> eventTable
                 = new Dictionary<EVENT, Action>();

    // Adds a delegate to get called for a specific event
    public static void AddHandler(EVENT evnt, Action action)
    {
        if (!eventTable.ContainsKey(evnt)) eventTable[evnt] = action;
        else eventTable[evnt] += action;
    }

    // Fires the event
    public static void Broadcast(EVENT evnt)
    {
        if (eventTable[evnt] != null) eventTable[evnt]();
    }
    #endregion
}

public static class EventManagerGen<T>
{
    public delegate void GenericDelegate<T>(T c);

    // Stores the delegates that get called when an event is fired
    static Dictionary<EVENT, GenericDelegate<T>> genericEventTable = new Dictionary<EVENT, GenericDelegate<T>>();
    
    // Adds a delegate to get called for a specific event
    public static void AddHandler(EVENT evnt, GenericDelegate<T> action)
    {
        if (!genericEventTable.ContainsKey(evnt)) genericEventTable[evnt] = action;
        else genericEventTable[evnt] += action;
    }

    // Fires the event
    public static void Broadcast(EVENT evnt, T c)
    {
        if (genericEventTable[evnt] != null) genericEventTable[evnt](c);
    }
}



