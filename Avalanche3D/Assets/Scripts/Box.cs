using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Box : MonoBehaviour
{
    Rigidbody rigidBody;
    public bool IsPlaced;
    public bool CanBeHalted;

    private void Start()
    {
        float randomSize = Random.Range(1,3.1f);
        transform.localScale = new Vector3(randomSize, randomSize, randomSize);
        rigidBody = GetComponent<Rigidbody>();
        CanBeHalted = true;
    }
    private void OnCollisionEnter(Collision collision)
    {
        if(CanBeHalted)
        {           
            if(collision.gameObject.tag == "Box" || collision.gameObject.tag == "Ground" || collision.gameObject.tag == "Player")
            {
                IsPlaced = true;
                rigidBody.isKinematic = true;
            }
        }
    }
}
