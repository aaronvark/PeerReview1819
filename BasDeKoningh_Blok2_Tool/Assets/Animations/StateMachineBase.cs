﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace EasyAI
{
    public abstract class StateMachineBase : StateMachineBehaviour
    {
        public TemperamentData temperamentData;
        public WayPointData wayPointData;
        public AiSettingData aiSettingData;
        public Transform playerPos;
        public float attackRange = 0;
        public float moodRange = 0;
        protected AnimationData animationData;
        protected AnimatorOverrideController animatorOverrideController;

        protected AnimationClipOverrides clipOverrides;
        public override void OnStateEnter(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
        {
            playerPos = GameObject.FindGameObjectWithTag("Player").transform;
            temperamentData = animator.GetComponent<TemperamentData>();
            animationData = animator.GetComponent<AnimationData>();
            aiSettingData = animator.GetComponent<AiSettingData>();
            wayPointData = animator.GetComponent<WayPointData>();
            attackRange = TemperamentManager.CombatTriggerRange(temperamentData.CombatStyle);
            moodRange = TemperamentManager.MoodTriggerRange(temperamentData.Mood);
            animatorOverrideController = new AnimatorOverrideController(animator.runtimeAnimatorController);

            //animatorOverrideController["Walking"] = animationData.AttackAnimation;
            
            clipOverrides = new AnimationClipOverrides(animatorOverrideController.overridesCount);
            animatorOverrideController.GetOverrides(clipOverrides);
            

            clipOverrides["Idle"] = animationData.IdleAnimation;
            clipOverrides["Walking"] = animationData.WalkAnimation;
            clipOverrides["Running"] = animationData.RunAnimation;
            clipOverrides["Attacking"] = animationData.AttackAnimation;
            clipOverrides["Death"] = animationData.DeathAnimation;
            animatorOverrideController.ApplyOverrides(clipOverrides);
            animator.runtimeAnimatorController = animatorOverrideController;

        }

        public override void OnStateUpdate(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
        {

        }

        public override void OnStateExit(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
        {

        }
    }
}
    

