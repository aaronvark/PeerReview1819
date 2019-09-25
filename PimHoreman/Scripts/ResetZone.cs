using UnityEngine;

/// <summary>
/// ResetZone class, if the ball reaches the trigger, reset the ball trough the interface IReset.
/// </summary>
public class ResetZone : MonoBehaviour
{
	[SerializeField] private Vector3 startPosition;

	private void OnTriggerEnter(Collider collision)
	{
		if (collision.gameObject.tag == Tags.Ball)
		{
			IReset iReset = (IReset)collision.gameObject.GetComponent(typeof(IReset));

			if (iReset != null)
			{
				iReset.ResetPosition = startPosition;
				iReset.ResetObject();
			}
		}
	}
}