using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Box : MonoBehaviour
{
    Rigidbody rigidbody;
    public bool IsPlaced;
    public bool CanBeHalted;

    private void Awake()
    {
        rigidbody = GetComponent<Rigidbody>();
    }

    private void OnEnable()
    {
        Initialize();
    }

    //Randomize size
    void Initialize()
    {
        float randomSize = Random.Range(1, 3.1f);
        transform.localScale = new Vector3(randomSize, randomSize, randomSize);
        CanBeHalted = true;
    }

    //Set rigidbody to kinematic when hitting something
    private void OnCollisionEnter(Collision collision)
    {
        if(CanBeHalted)
        {           
            if(collision.gameObject.tag == "Box" || collision.gameObject.tag == "Ground" || collision.gameObject.tag == "Player")
            {
                IsPlaced = true;
                rigidbody.isKinematic = true;
            }
        }
    }
}
