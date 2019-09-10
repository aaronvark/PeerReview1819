﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public enum EVENT { gameUpdateEvent, MyEvent2 }; // ... Other events
public static class EventManager
{
    //Static delegates
    public delegate void OnUpdateCaller();
    public static OnUpdateCaller OnLevelUpdateHandler { get; set; }

    public delegate void OnEnemyHit();
    public static OnEnemyHit OnEnemyHitHandler { get; set; }

    #region Different way for handling events
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