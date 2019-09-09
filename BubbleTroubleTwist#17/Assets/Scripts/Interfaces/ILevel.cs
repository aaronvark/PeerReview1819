using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Bas.Interfaces
{
    public delegate void OnLevelUpdate();

    public interface ILevel
    {
        void UpdateLevel();
        void AddPlayer(GameObject player);
    }
}
