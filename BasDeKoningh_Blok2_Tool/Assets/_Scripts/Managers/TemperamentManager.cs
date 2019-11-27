using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Linq;

namespace EasyAI
{
    public static class TemperamentManager
    {
        public static float MoodTriggerRange(Mood mood)
        {
            switch(mood)
            {
                case Mood.Aggresive:
                    return 20;
                case Mood.AtEase:
                    return 10;
                case Mood.Sleeping:
                    return 1;
                default:
                    return 10;
            }
        }

        public static float CombatTriggerRange(CombatStyle combatStyle)
        {
            switch(combatStyle)
            {
                case CombatStyle.Defensive:
                    return 2;
                case CombatStyle.InBetween:
                    return 5;
                case CombatStyle.Offensive:
                    return 7;
                default:
                    return 5;
            }
        }

        public static int WanderTypeGiver(WanderType wanderType, WayPointData wayPointData, Vector3 nextPoint)
        {
            switch(wanderType)
            {
                case WanderType.Random:
                    return Random.Range(0, wayPointData.WayPoints.Count);
                case WanderType.Loop:
                    var index = wayPointData.WayPoints.FindIndex(wp => wp.ToZeroY().Equals(nextPoint))+1;
                    if(index > wayPointData.WayPoints.Count)
                    {
                        return 0;
                    }
                    else
                    {
                        return index;
                    }
                case WanderType.Once:
                    var incomingIndex = wayPointData.WayPoints.FindIndex(wp => wp.ToZeroY().Equals(nextPoint));
                    if (incomingIndex+1 > wayPointData.WayPoints.Count)
                    {
                        return incomingIndex;
                    }
                    else
                    {
                        return (incomingIndex+1);
                    }
                default:
                    return 0;
            }
        }
    }
}
