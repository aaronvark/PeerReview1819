using Breakin.GameManagement;
using UnityEngine;

namespace Breakin
{
    [RequireComponent(typeof(Rigidbody2D))]
    public class Ball : MonoBehaviour
    {
        [SerializeField] private float speed = 0.5f;
        [SerializeField] private Bat bat;
        [SerializeField] private int lives;

        private bool locked = true;
        private Vector2 startPos;
        private Vector2 startScale;

        private Rigidbody2D rb;

        /// <summary>
        /// The number of lives of this game. When this is set to a value lower than 0, the GameOver function is called.
        /// </summary>
        private int Lives
        {
            get => lives;
            set
            {
                lives = value;

                // Run game over sequence once the lives have reached a value lower than 0
                if (lives < 0) GameOver();
            }
        }

        private void Start()
        {
            rb = GetComponent<Rigidbody2D>();

            // Save the position and the scale at the start of the game, to reapply every time the ball is reset to the bat
            startPos = transform.localPosition;
            startScale = transform.localScale;

            // The ball should start locked at to the bat at the beginning of the game
            Lock();

            EventManager.gameUpdate += OnUpdate;
        }

        private void OnUpdate()
        {
            if (locked)
            {
                // Check if the player clicked the mouse button and if so, unlock the ball
                if (CheckMouseClick()) Unlock();
            }

            // Keep checking if the ball has gone out of the playing field
            if (!locked) CheckOutOfRange();
        }

        /// <summary>
        /// Checks if the mouse button was clicked.
        /// </summary>
        private bool CheckMouseClick()
        {
            return Input.GetMouseButtonDown(0);
        }

        /// <summary>
        /// Checks if the ball has left the playing field and if it has, subtract a life and reset the ball to the bat.
        /// </summary>
        private void CheckOutOfRange()
        {
            float _margin = .5f;
            // A life is subtracted when the ball is no longer inside the range of the bat. This adds a little margin (0.5)
            // to add a slight delay between leaving this range and actually losing a life/getting reset
            if (Vector2.Distance(transform.position, Vector2.zero) > bat.Radius + _margin)
            {
                Lives -= 1;
                Lock();
            }
        }

        /// <summary>
        /// Unlocks the ball from the bat by removing it from the bat as a child and giving it velocity.
        /// </summary>
        private void Unlock()
        {
            locked = false;

            // Add velocity in the inverse direction of the bat (inwards) with a magnitude of the speed parameter
            rb.velocity = -bat.Dir * speed;

            // Remove bat as parent to move the ball freely
            transform.SetParent(null);
        }

        /// <summary>
        /// Locks the ball to the bat by parenting it to the bat and removing the velocity.
        /// </summary>
        private void Lock()
        {
            locked = true;

            // Stop any possible ball movement
            rb.velocity = Vector2.zero;

            // Parent ball to bat to give the feeling the ball is attached to the bat
            transform.SetParent(bat.transform);
            // Re-initialize all the transform values to their defaults
            transform.localPosition = startPos;
            transform.localRotation = Quaternion.Euler(0, 0, 0);
            transform.localScale = startScale;
        }

        /// <summary>
        /// Starts the game over sequence
        /// TODO
        /// </summary>
        private void GameOver()
        {
            // TODO placeholder, replace with real game over screen
            Debug.Log("Game over");
        }
    }
}