using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DestructableWall : MonoBehaviour, IDamagable {

    public void Damage() {
        Die();
    }

    public void Die() {
        Debug.Log("DIE");
        WallManager.Instance.RemoveFromList(this);
        Destroy(this.gameObject);
    }
}
