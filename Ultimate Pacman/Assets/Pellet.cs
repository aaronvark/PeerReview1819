using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Pellet : MonoBehaviour
{
    [SerializeField]
    private int points = 10;

    public void Consume() {
        Destroy(gameObject);
    }
}
