using Breakin.Pooling;
using Breakin.Sound;
using UnityEngine;

namespace Breakin
{
    public delegate void BlockBreak();

    public abstract class Block : MonoBehaviour, IPoolable
    {
        /// <summary>
        /// Invoked when the block breaks
        /// </summary>
        public event BlockBreak BlockBroken;

        private bool isActive;

        public bool IsActive
        {
            get => isActive;
            set
            {
                // If active is being set to false, all event listeners should be removed to enable the instance
                // to be reused
                if (!value) BlockBroken = null;

                gameObject.SetActive(value);
                isActive = value;
            }
        }

        // Set the color of the block
        // TODO temp function, to be improved or removed
        public void SetColor(Color c)
        {
            SpriteRenderer _sr = GetComponent<SpriteRenderer>();
            _sr.color = c;
        }

        /// <summary>
        /// This function is called each time the ball hits a block
        /// </summary>
        protected abstract void OnHit();

        /// <summary>
        /// This function should be called when the block breaks. It marks the block as being broken and invokes the
        /// BlockBroken event
        /// </summary>
        protected void OnBreak()
        {
            BlockBroken?.Invoke();
            gameObject.SetActive(false);
        }

        private void OnCollisionEnter2D(Collision2D other)
        {
            if (other.gameObject.CompareTag("Ball")) OnHit();
        }
    }
}