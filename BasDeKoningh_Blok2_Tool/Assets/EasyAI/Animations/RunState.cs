using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace EasyAI
{
    public class RunState : StateMachineBase
    {
 

        public override void OnStateEnter(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
        {
            base.OnStateEnter(animator, stateInfo, layerIndex);

        }

        public override void OnStateUpdate(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
        {
            var step = AiSettingData.Speed * Time.deltaTime;

            animator.transform.position = Vector3.MoveTowards(animator.transform.position, PlayerPos.position, step);

            var playerDistance = PlayerPos.position - animator.transform.position;
            Vector3 newDir = Vector3.RotateTowards(animator.transform.forward, playerDistance, step, 0.0f);
            if (playerDistance.sqrMagnitude < MoodRange * MoodRange)
            {
                //Player is within mood range.
                animator.SetBool("Walking", false);
                animator.SetBool("Running", true);
                animator.transform.rotation = Quaternion.LookRotation(newDir);
            }
            if (playerDistance.sqrMagnitude < AttackRange * AttackRange)
            {
                //Player is within Attack range
                animator.SetBool("Running", false);
                animator.SetBool("Attacking", true);
            }
            if (playerDistance.sqrMagnitude > MoodRange * MoodRange)
            {
                //Player is out of range.
                animator.SetBool("Running", false);
                animator.SetBool("Walking", true);
            }
        }

        public override void OnStateExit(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
        {

        }
    }
}
