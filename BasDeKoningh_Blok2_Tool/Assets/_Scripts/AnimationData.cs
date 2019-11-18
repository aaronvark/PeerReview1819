using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace EasyAI
{
    [System.Serializable]
    public class AnimationData
    {
        [Header("MovementAnimations:")]
        [SerializeField]
        public AnimationClip IdleAnimation;
        [SerializeField]
        public AnimationClip WalkAnimation;
        [SerializeField]
        public AnimationClip RunAnimation;

        [Header("CombatAnimations:")]
        [SerializeField]
        public AnimationClip AttackAnimation;
        [SerializeField]
        public AnimationClip DeathAnimation;
    }
}
