using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace EasyAI
{
    public class AttackState : StateMachineBase
    {
        public override void OnStateEnter(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
        {
            base.OnStateEnter(animator, stateInfo, layerIndex);
        }

        public override void OnStateUpdate(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
        {
            var playerDistance = PlayerPos.position - animator.transform.position;

            if (playerDistance.sqrMagnitude > AttackRange * AttackRange)
            {
                //Player is out of range.
                animator.SetBool("Attacking", false);
                animator.SetBool("Running", true);
            }
            else
            {
                animator.SetBool("Attacking", true);
            }
        }

        public override void OnStateExit(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
        {

        }
    }
}
