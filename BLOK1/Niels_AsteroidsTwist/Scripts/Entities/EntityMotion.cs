using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EntityMotion : MonoBehaviour
{
    [SerializeField]
    private float speed;
    private Rigidbody2D rb;
    [SerializeField]
    private bool useRandomSpeed;

    public void ApplyForwardForce()
    {
        if (useRandomSpeed)
        {
            float _randomSpeed = Random.Range(1150, 2000);
            _randomSpeed = Mathf.Round((_randomSpeed / 100)) * 100;
            speed = _randomSpeed;
        }
        rb.velocity = Vector2.zero;
        rb.angularVelocity = 0f;

        // Small delay to prevent code execution bug
        Invoke("AddForce", .02f);
    }

    public void AddForce()
    {
        rb.AddRelativeForce(new Vector2(0, speed));
    }

    private void Awake()
    {
        rb = GetComponent<Rigidbody2D>();
    }

    // adds motion to the entity on Enable
    private void OnEnable()
    {
        ApplyForwardForce();
    }

    private void OnDisable()
    {
        rb.velocity = Vector2.zero;
        rb.angularVelocity = 0f;
    }


}
