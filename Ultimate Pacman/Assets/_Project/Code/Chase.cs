using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Chase : MonoBehaviour
{
    [SerializeField]
    private Transform target = null;
    [SerializeField]
    private float moveSpeed = 2f;

    // TODO - create a single script that handles movement!

    private void FixedUpdate() {
        ChaseTarget();
    }

    private void ChaseTarget() {
        if (!target)
            return;

        Vector2 distance = target.position - transform.position;
        Vector3 direction = distance.normalized;
        transform.position += direction * moveSpeed * Time.deltaTime;
    }
}
