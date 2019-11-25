using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace EasyAI
{
    public class WalkState : StateMachineBase
    {
        private float progress;
        private bool goingForward = true;
        public override void OnStateEnter(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
        {
            base.OnStateEnter(animator, stateInfo, layerIndex);
        }

        public override void OnStateUpdate(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
        {
            if (goingForward)
            {
                progress += Time.deltaTime / wayPointData.duration;
                if (progress > 1f)
                {
                    if (wayPointData.mode == SplineWalkerMode.Once)
                    {
                        progress = 1f;
                    }
                    else if (wayPointData.mode == SplineWalkerMode.Loop)
                    {
                        progress -= 1f;
                    }
                    else
                    {
                        progress = 2f - progress;
                        goingForward = false;
                    }
                }
            }
            else
            {
                progress -= Time.deltaTime / wayPointData.duration;
                if (progress < 0f)
                {
                    progress = -progress;
                    goingForward = true;
                }
            }

            Vector3 position = wayPointData.spline.GetPoint(progress);
            animator.transform.localPosition = position;
            if (wayPointData.lookForward)
            {
                animator.transform.LookAt(position + wayPointData.spline.GetDirection(progress));
            }
            var playerDistance = playerPos.position - animator.transform.position;

        
/*
        Vector3 nextPoint = wayPointData.WayPoints[wayPointData.WayPointIndex];
            var step = aiSettingData.Speed * Time.deltaTime;
            Vector3 newDir = Vector3.RotateTowards(animator.transform.forward, nextPoint, step, 0.0f);
            var playerDistance = playerPos.position - animator.transform.position;

            if (Vector3.Distance(animator.transform.position.ToZeroY(), nextPoint.ToZeroY()) > 0.2f)
            {
                animator.transform.position = Vector3.MoveTowards(animator.transform.position, nextPoint, step);
                animator.transform.rotation = Quaternion.LookRotation(newDir);
            }
            else
            {
                wayPointData.WayPointIndex = Random.Range(0, wayPointData.WayPoints.Count);
            }
            */
            if (playerDistance.sqrMagnitude < moodRange * moodRange)
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
