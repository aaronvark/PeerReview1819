using Breakin.Pooling;
using UnityEngine;

namespace Breakin
{
	public abstract class Block : MonoBehaviour, IPoolable
	{
		private bool isActive;
		public bool IsActive
		{
			get => isActive;
			set
			{
				gameObject.SetActive(value);
				isActive = value;
			}
		}

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