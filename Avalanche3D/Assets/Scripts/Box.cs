﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Box : MonoBehaviour
{
    //References
    Rigidbody rigidbody;
    MeshRenderer meshRenderer;
    
    //Public Variables
    public bool IsPlaced;
    public bool CanBeHalted;

    private void Awake()
    {
        rigidbody = GetComponent<Rigidbody>();
        meshRenderer = GetComponent<MeshRenderer>();
    }

    private void OnEnable()
    {
        Initialize();
        ChangeColor();
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
        if (CanBeHalted)
        {
            if (collision.gameObject.tag == "Box" || collision.gameObject.tag == "Ground")
            {
                IsPlaced = true;
                rigidbody.isKinematic = true;
            }
        }
    }

    void ChangeColor()
    {
        meshRenderer.material.color = new Color32((byte)Random.Range(0, 256), (byte)Random.Range(0, 256), (byte)Random.Range(0, 256), 255);
    }
}
