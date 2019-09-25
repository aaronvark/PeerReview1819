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
        void CreateLevel(GameObject prefab, Vector3 position);
        void ClearPlayers();
        List<Level> GiveLevels();
        Level LastPlayedLevel();
    }
}
