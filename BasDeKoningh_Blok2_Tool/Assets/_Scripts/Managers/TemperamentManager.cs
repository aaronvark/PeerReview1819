using System.Collections;
using System.Collections.Generic;
using UnityEngine;

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


    }
}
