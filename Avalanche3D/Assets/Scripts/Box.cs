using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Box : MonoBehaviour
{
    Rigidbody rigidBody;
    public bool IsPlaced;
    public bool CanBeHalted;

    private void Awake()
    {
        rigidBody = GetComponent<Rigidbody>();
    }

    private void OnEnable()
    {
        Initialise();
    }

    void Initialise()
    {
        float randomSize = Random.Range(1, 3.1f);
        transform.localScale = new Vector3(randomSize, randomSize, randomSize);
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
