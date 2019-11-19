using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace EasyAI
{
    public class WalkState : StateMachineBase
    {
        

        public override void OnStateEnter(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
        {
            base.OnStateEnter(animator, stateInfo, layerIndex);
        }

        public override void OnStateUpdate(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
        {
            Vector3 nextPoint = wayPointData.WayPoints[wayPointData.WayPointIndex].position;
            var step = aiSettingData.Speed * Time.deltaTime;
            Vector3 newDir = Vector3.RotateTowards(animator.transform.forward, nextPoint.ToZeroY(), step, 0.0f);
            var playerDistance = playerPos.position - animator.transform.position;

            if (Vector3.Distance(animator.transform.position.ToZeroY(), nextPoint) > 0.2f)
            {
                animator.transform.position = Vector3.MoveTowards(animator.transform.position.ToZeroY(), nextPoint.ToZeroY(), step);
            }
            else
            {
                wayPointData.WayPointIndex = Random.Range(0, wayPointData.WayPoints.Count);
            }

            if (playerDistance.sqrMagnitude < MaxRange * MaxRange)
            {
                animator.SetBool("Walking", false);
                animator.SetBool("Running", true);
            }
        }

        public override void OnStateExit(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
        {

        }
    }
}
