using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EntityAutoDestroyer : MonoBehaviour
{
    [SerializeField]
    private float destroyTime;

    // script that destroys the entity after a specific amount of time after spawning
    private void Start() { 
        StartCoroutine(DestroyObj(destroyTime));
    }

    IEnumerator DestroyObj(float waitTime) { 
        yield return new WaitForSeconds(waitTime);
        Destroy(this.gameObject);
    }
}
