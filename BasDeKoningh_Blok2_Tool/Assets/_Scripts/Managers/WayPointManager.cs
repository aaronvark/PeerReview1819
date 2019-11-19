using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace EasyAI
{
    public class WayPointManager : ManagerBase
    {
        public List<WayPointData> wayPointDatas;
        public float speed = 1f;
        private int wayPointIndex = 0;

        private void Awake()
        {
            EventManager<MonoBehaviour>.AddHandler(EVENT.setBehaviour, Run);
        }
        public override void Run(MonoBehaviour reference)
        {
            WayPointData wayPointData = reference as WayPointData;
            wayPointDatas.Add(wayPointData);
            foreach (var wayPointref in wayPointDatas)
            {
                wayPointref.StartCoroutine(Move(wayPointref));
            }
        }

        private IEnumerator Move(WayPointData wayPointData)
        {
            yield return new WaitForEndOfFrame();
            float step = speed * Time.deltaTime;
            Vector3 newPosition = wayPointData.WayPoints[wayPointIndex].position;
            while(Vector3.Distance(wayPointData.gameObject.transform.position.ToZeroY(), newPosition.ToZeroY()) > 0.1f)
            {
                GameObject target = wayPointData.gameObject;
                //target.transform.position = Vector3.MoveTowards(target.transform.position, newPosition, speed);
                target.transform.LerpTransform(wayPointData, newPosition, step);
            }
            if (wayPointIndex < wayPointData.WayPoints.Count-1)
                wayPointIndex++;
            else
                wayPointIndex = 0;
            StartCoroutine(Move(wayPointData));
        }
    }
}
