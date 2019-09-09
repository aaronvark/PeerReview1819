using UnityEngine;

public class GravityTrigger : MonoBehaviour
{
	private void OnTriggerStay(Collider other)
	{
		if(other.gameObject.tag == Tags.Ball)
		{
			other.GetComponent<Rigidbody>().useGravity = true;
		}
	}

	private void OnTriggerExit(Collider other)
	{
		if(other.gameObject.tag == Tags.Ball)
		{
			other.GetComponent<Rigidbody>().useGravity = false;
		}
	}
}