using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace EasyAI
{
    public class WalkState : StateMachineBase
    {
        Vector3 nextPoint = Vector3.zero;
        public override void OnStateEnter(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
        {
            base.OnStateEnter(animator, stateInfo, layerIndex);

        }

        public override void OnStateUpdate(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
        {
            var playerDistance = PlayerPos.position - animator.transform.position;
            if(WayPointData.WayPointIndex >= WayPointData.WayPoints.Count)
            {
                animator.SetBool("Walking", false);
                animator.SetBool("Runniing", false);
                return;
            }
            nextPoint = WayPointData.WayPoints[WayPointData.WayPointIndex];

            var step = AiSettingData.Speed * Time.deltaTime;
            Vector3 newDir = Vector3.RotateTowards(animator.transform.forward, nextPoint, step, 0.0f);

            if (Vector3.Distance(animator.transform.position.ToZeroY(), nextPoint.ToZeroY()) > 0.02f)
            {
                animator.transform.position = Vector3.MoveTowards(animator.transform.position, nextPoint, step);
                //animator.transform.rotation = Quaternion.LookRotation(newDir);
                animator.transform.LookAt(nextPoint);
            }
            else
            {
                WayPointData.WayPointIndex = TemperamentManager.WanderTypeGiver(WayPointData.WanderType, WayPointData, nextPoint);
            }

            if (playerDistance.sqrMagnitude < MoodRange * MoodRange)
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
