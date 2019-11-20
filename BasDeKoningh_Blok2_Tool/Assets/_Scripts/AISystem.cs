using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;

namespace EasyAI
{ 
    public enum Mood
    {
        Aggresive = 0,
        AtEase = 1,
        Sleeping = 2
    }

    public enum Confidence
    {
        Leader = 0,
        Mediocre = 1,
        Pussy = 2
    }

    public enum WanderType
    {
        Dynamic = 0,
        Static = 1,
        Fixed = 2
    }

    public enum CombatStyle
    {
        Offensive = 0,
        Defensive = 1,
        InBetween = 2
    }

    public enum AiType
    {
        Close = 0,
        Ranged = 1,
        Medic = 2
    }
    
    

    [RequireComponent(typeof(Rigidbody))]
    [RequireComponent(typeof(BoxCollider))]
    [RequireComponent(typeof(Animator))]
    public class AISystem : MonoBehaviour
    {
       
    }
}