using UnityEngine;

/// <summary>
/// ScreenShake class, When the ball hits an object, the object fires an Action to the screenshake class to send a screenshake.
/// </summary>
public class ScreenShake : MonoBehaviour
{
	[SerializeField] private Transform camTransform;
	[SerializeField] private float shakeDuration = 0f;
	[SerializeField] private float shakeAmount = 0.7f;
	[SerializeField] private float decreaseFactor = 1.0f;
	[SerializeField] private Vector3 originalPos;

	private bool isActivated = false;

	private void ShakeScreen()
	{
		isActivated = true;
	}

	private void Update()
	{
		if(isActivated)
		{
			if (shakeDuration > 0)
			{
				camTransform.localPosition = originalPos + Random.insideUnitSphere * shakeAmount;
				shakeDuration -= Time.deltaTime * decreaseFactor;
			}
			else
			{
				shakeDuration = shakeAmount;
				camTransform.localPosition = originalPos;
				isActivated = false;
			}
		}
	}

	private void OnEnable()
	{
		Bouncer.BouncerScreenshakeEvent += ShakeScreen;
	}

	private void OnDisable()
	{
		Bouncer.BouncerScreenshakeEvent -= ShakeScreen;
	}
}