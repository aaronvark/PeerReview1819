using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Bas.Interfaces
{
    //public delegate void OnStatsHandler<T>(T stats);

    public interface IStats<T>
    {
        //event OnStatsHandler<T> onStatsHandler;
        void SetStats(T data);
    }
}