using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EntityMotion : MonoBehaviour
{
    [SerializeField]
    private float speed;
    private Rigidbody2D rb;

    // adds motion to the entity on Instantiation
    private void Start() {
        rb = GetComponent<Rigidbody2D>();
        rb.AddRelativeForce(new Vector2(0, speed));
    }

}
