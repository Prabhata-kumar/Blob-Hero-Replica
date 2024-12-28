using UnityEngine;

namespace BlobHero
{
    public class EnemyMovement : MonoBehaviour
    {
        public FlowFieldController flowFieldController;  // Reference to the flow field controller
        public float moveSpeed = 2f;                    // Speed of the enemy's movement

        void Start()
        {
            // Ensures flow field controller is assigned
            if (flowFieldController == null)
            {
                Debug.LogError("FlowFieldController is not assigned to the enemy.");
            }
        }

        void Update()
        {
            // Only update the enemy's movement if it's active in the scene
            if (!gameObject.activeInHierarchy) return;

            // Get the flow direction from the flow field for the current position
            Vector2 flowDirection = flowFieldController.GetFlowDirection(transform.position);

            // If flow direction is valid (not zero), move the enemy accordingly
            if (flowDirection != Vector2.zero)
            {
                MoveTowardsDirection(flowDirection);
            }
        }

        private void MoveTowardsDirection(Vector2 direction)
        {
            // Move the enemy based on the flow field direction in the 2D world
            transform.position += (Vector3)(direction * moveSpeed * Time.deltaTime);
        }
    }
}
