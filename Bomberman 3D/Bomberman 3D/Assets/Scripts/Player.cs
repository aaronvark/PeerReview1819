using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : Actor
{

    public override void Damage()
    {
        base.Damage();
        uiManager.DecreaseHearts(1, this.health);
        Debug.Log(health);
    }

    public override void Die()
    {
        base.Die();
        Debug.Log("Player Dies");
        GameManager.Instance.Reload();
    }
}
