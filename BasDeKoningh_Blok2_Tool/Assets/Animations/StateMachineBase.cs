using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace EasyAI
{
    public abstract class StateMachineBase : StateMachineBehaviour
    {
        public WayPointData wayPointData;
        public AiSettingData aiSettingData;
        public Transform playerPos;
        protected AnimationData animationData;
        protected AnimatorOverrideController animatorOverrideController;
        
        protected AnimationClipOverrides clipOverrides;
        public override void OnStateEnter(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
        {
            animationData = animator.GetComponent<AnimationData>();
            aiSettingData = animator.GetComponent<AiSettingData>();
            wayPointData = animator.GetComponent<WayPointData>();
            animatorOverrideController = new AnimatorOverrideController(animator.runtimeAnimatorController);
            animator.runtimeAnimatorController = animatorOverrideController;

            clipOverrides = new AnimationClipOverrides(animatorOverrideController.overridesCount);
            animatorOverrideController.GetOverrides(clipOverrides);

            clipOverrides["Idle"] = animationData.IdleAnimation;
            clipOverrides["Walk"] = animationData.WalkAnimation;
            clipOverrides["Run"] = animationData.RunAnimation;
            clipOverrides["Attack"] = animationData.AttackAnimation;
            clipOverrides["Dead"] = animationData.DeathAnimation;
            animatorOverrideController.ApplyOverrides(clipOverrides);
        }

        public override void OnStateUpdate(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
        {

        }

        public override void OnStateExit(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
        {

        }
    }
}
    

