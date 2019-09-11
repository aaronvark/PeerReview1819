using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerDamagable : DamagableEntity
{
    protected override void Die()
    {
        gameObject.SetActive(false);
    }
}
