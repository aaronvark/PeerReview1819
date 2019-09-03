using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BomExplode : MonoBehaviour
{
    [SerializeField] private float explodeTime;

    // Start is called before the first frame update
    void Start()
    {
        StartCoroutine(ExplodeTime());
    }

    IEnumerator ExplodeTime()
    {
        yield return new WaitForSeconds(explodeTime);
        Debug.Log("I got destroyed");
        Destroy(this);
    }
}
