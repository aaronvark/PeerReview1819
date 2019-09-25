using UnityEngine;

/// <summary>
/// Check if the ball is in the trigger, if it is, then apply gravity, if not then uncheck the gravity.
/// </summary>
public class GravityTrigger : MonoBehaviour
{
	[SerializeField] private GameObject gate;
	
	private void OnTriggerStay(Collider other)
	{
		if (other.gameObject.tag == Tags.Ball)
		{
			gate.SetActive(false);
			other.GetComponent<Rigidbody>().useGravity = true;
		}
	}

	private void OnTriggerExit(Collider other)
	{
		if(other.gameObject.tag == Tags.Ball)
		{
			gate.SetActive(true);
			other.GetComponent<Rigidbody>().useGravity = false;
		}
	}
}