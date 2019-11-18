using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace EasyAI
{
    public class TemperamentManager : ManagerBase
    {
        TemperamentData temperamentData;
        public override void Run(MonoBehaviour reference)
        {
            temperamentData = reference as TemperamentData;
        }

        public void Update()
        {
            //we need to act on the given data
            //somewhere we need to move the npc(I think in waypointsystem)
        }
    }
}
