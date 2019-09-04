using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Actor : MonoBehaviour {

    public virtual void Death() {
    }

    private void OnCollisionStay2D(Collision2D collision) {
        if(collision.gameObject.name == "Player") {
            Death();
        }
    }
}
