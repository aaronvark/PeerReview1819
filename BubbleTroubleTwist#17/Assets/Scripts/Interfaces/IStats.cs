using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Bas.Interfaces
{
    public interface IStats<T>
    {
        void SetStats(T data);
    }
}