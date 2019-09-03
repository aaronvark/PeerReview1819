using UnityEngine;

public class BatMovement : MonoBehaviour {

	[SerializeField] private float radius;

	private float angle;
	private Camera cam;

	private void Start() {
		cam = GameObject.FindWithTag("MainCamera").GetComponent<Camera>();
	}

	private void Update() {
		GetAngleFromMousePos();

		SetRotation();
		SetPosition();
	}

	private void GetAngleFromMousePos() {
		// Get the mouse pos in pixels
		Vector3 _mousePos = Input.mousePosition;
		// Turn the pixels into a point in the game world; use Vector2 to discard z-coordinate (not used in 2d)
		Vector2 _mousePosWorld = cam.ScreenToWorldPoint(_mousePos);
		
		// The angle of the bat should be relative to the positive x-axis (same as on a unit circle)
		angle = Vector2.SignedAngle(Vector2.right, _mousePosWorld.normalized);
	}

	private void SetRotation() {
		transform.localRotation = Quaternion.Euler(0, 0, angle);
	}

	private void SetPosition() {
		float _rad = angle * Mathf.Deg2Rad;
		Vector3 _newPos = new Vector3(Mathf.Cos(_rad), Mathf.Sin(_rad)) * radius;

		transform.position = _newPos;
	}

	private void OnDrawGizmos() {
		Gizmos.color = Color.red;
		Gizmos.DrawWireSphere(Vector3.zero, radius);
	}
}