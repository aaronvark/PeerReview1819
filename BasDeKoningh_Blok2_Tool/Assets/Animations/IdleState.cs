using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace EasyAI
{
    public class IdleState : StateMachineBase
    {
        public override void OnStateEnter(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
        {
            base.OnStateEnter(animator, stateInfo, layerIndex);
            animator.SetBool("Walking", true);
        }
    }
}
