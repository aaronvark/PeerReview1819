using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Pellet : MonoBehaviour
{
    private static int pelletCount = 0;

    [SerializeField]
    private int points = 10;

    private void OnEnable() {
        pelletCount++;
    }

    private void OnDisable() {
        pelletCount--;
    }

    public void Consume() {
        Destroy(gameObject);

        //FIX - You Win is a magic string -> use Score Manager instead
        if (pelletCount == 0) {
            SceneHandler.instance.LoadScene("You Win");
        }
    }
}
