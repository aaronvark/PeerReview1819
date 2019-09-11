using UnityEngine;

namespace Breakin
{
	public abstract class Block : MonoBehaviour
	{
		/// <summary>
		/// This function is called each time the ball hits a block
		/// </summary>
		protected abstract void OnHit();

		private void OnCollisionEnter2D(Collision2D other)
		{
			if (other.gameObject.CompareTag("Ball"))
				OnHit();
		}
	}
}