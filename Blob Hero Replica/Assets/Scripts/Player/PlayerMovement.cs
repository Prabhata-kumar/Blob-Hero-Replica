using UnityEngine;

namespace BlobHero
{
    public class PlayerMovement : MonoBehaviour
    {
        [Header("Movement Settings")]
        [SerializeField] private float moveSpeed = 5f; // Movement speed of the player
        [SerializeField] private bool useRigidbody = true; // Whether to use Rigidbody or direct Transform movement

        private Rigidbody2D rb;
        [SerializeField] private JoyStickController joystick;

        void Start()
        {
            // Get references to the required components
            //joystick = GetComponent<JoyStickController>();
            if (useRigidbody)
            {
                rb = GetComponent<Rigidbody2D>();
            }
        }

        void Update()
        {
            // Get joystick input
            float moveHorizontal = joystick.Horizontal;
            float moveVertical = joystick.Vertical;

            // Calculate the movement vector
            Vector3 movement = new Vector3(moveHorizontal, moveVertical, 0).normalized;

            // Apply movement based on whether using Rigidbody or Transform
            if (useRigidbody && rb != null)
            {
                // Move the player using Rigidbody for physics-based movement
                rb.MovePosition(transform.position + movement * moveSpeed * Time.deltaTime);
            }
            else
            {
                // Move the player using Transform (simple translation)
                transform.Translate(movement * moveSpeed * Time.deltaTime, Space.World);
            }
        }
    }
}