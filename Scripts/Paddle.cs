using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Paddle : MonoBehaviour
{
	private PaddleInput paddleInput;

    // Start is called before the first frame update
    void Start()
    {
		paddleInput = GetComponent<PaddleInput>();    
    }

    // Update is called once per frame
    void Update()
    {
		paddleInput.PaddleMovement();
    }
}
