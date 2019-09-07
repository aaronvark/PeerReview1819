using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Bas.Interfaces;

public class StatsBase<T> : IStats<T>
{
    public T entityStats;

    //public event OnStatsHandler<T> onStatsHandler;

    public void SetStats(T data)
    {
        entityStats = data;
    }
}
