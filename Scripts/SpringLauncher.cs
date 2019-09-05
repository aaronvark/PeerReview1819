using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpringLauncher : MonoBehaviour
{
	[SerializeField] private int forceLauncher = 5;
	private Vector3 startPosition;
	private Rigidbody rb;

	// Start is called before the first frame update
	private void Start()
    {
		rb = GetComponent<Rigidbody>();
    }

    // Update is called once per frame
	private void FixedUpdate()
    {
		StiffenSpring();
	}

	private void StiffenSpring()
	{
		if(Input.GetKeyDown(KeyCode.S) || Input.GetKeyDown(KeyCode.DownArrow)){
			//
		}
	}
}
