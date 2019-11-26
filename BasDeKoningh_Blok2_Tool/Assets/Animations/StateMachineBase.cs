using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace EasyAI
{
    public abstract class StateMachineBase : StateMachineBehaviour
    {
        public TemperamentData TemperamentData;
        public WayPointData WayPointData;
        public AiSettingData AiSettingData;
        public Transform PlayerPos;
        public float AttackRange = 0;
        public float MoodRange = 0;

        protected AnimationData animationData;
        protected AnimatorOverrideController animatorOverrideController;
        protected AnimationClipOverrides clipOverrides;
        public override void OnStateEnter(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
        {
            PlayerPos = GameObject.FindGameObjectWithTag("Player").transform;
            TemperamentData = animator.GetComponent<TemperamentData>();
            animationData = animator.GetComponent<AnimationData>();
            AiSettingData = animator.GetComponent<AiSettingData>();
            WayPointData = animator.GetComponent<WayPointData>();
            AttackRange = TemperamentManager.CombatTriggerRange(TemperamentData.CombatStyle);
            MoodRange = TemperamentManager.MoodTriggerRange(TemperamentData.Mood);
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
    

