using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace EasyAI
{
    [System.Serializable]
    public class WayPointData
    {
        [SerializeField]
        public WanderType WanderType;
        [SerializeField]
        public List<Transform> WayPoints;
    }
}