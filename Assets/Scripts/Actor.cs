using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Actor : MonoBehaviour
{
    private Camera mainCamera;
    
    //Sets the local Camera as the main camera in the scene.
    private void Awake()
    {
        mainCamera = Camera.main;
    }

    //Sets the position of the actor to the other side of the screen.
    private void Update()
    {
        Vector2 viewPos = mainCamera.WorldToViewportPoint(transform.position);

        if (viewPos.x >= 1.1)
        {
            transform.position = new Vector2(-(transform.position.x - 0.3f), transform.position.y);
        }

        if (viewPos.x <= -0.1)
        {
            transform.position = new Vector2(-(transform.position.x + 0.3f), transform.position.y);
        }

        if (viewPos.y >= 1.1)
        {
            transform.position = new Vector2(transform.position.x, -(transform.position.y - 0.3f));
        }

        if (viewPos.y <= -0.1)
        {
            transform.position = new Vector2(transform.position.x, -(transform.position.y + 0.3f));
        }
    }

    //Setup for later.
    public virtual void Death()
    {
    }
}
