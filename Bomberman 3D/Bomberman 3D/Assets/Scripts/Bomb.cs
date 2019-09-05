using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bomb : MonoBehaviour {

    [SerializeField] private float explodeTime;
    [SerializeField] private float bomTimer;
    [SerializeField] private float raycastLength;

    Actor actor;
    RaycastHit[] hit;
    Vector3[] directions;

    private void Start() {
        actor = Actor.FindObjectOfType<Actor>();
        directions = new Vector3[] { Vector3.left, Vector3.right, Vector3.forward, Vector3.back };
        hit = new RaycastHit[directions.Length];
    }

    private void Update() {
        Timer();

        if (Timer() < 0) {
            Explode();
        }
    }

    private float Timer() {
        return bomTimer -= Time.deltaTime;
    }

    private void Explode() {

        for (int i = 0; i < directions.Length; i++) {
            if (Physics.Raycast(transform.position, directions[i], out hit[i], raycastLength)) {
                Debug.DrawRay(transform.position, directions[i], Color.red);

                try {
                    hit[i].collider.GetComponent<IDamagable>().Damage();
                }
                catch {

                }
            }
        }

        actor.bombDeployCheck = true;

        Debug.Log("I got destroyed");
        Destroy(this.gameObject);
    }
}
