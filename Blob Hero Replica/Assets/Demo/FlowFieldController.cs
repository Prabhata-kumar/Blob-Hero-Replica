using UnityEngine;

namespace BlobHero
{
    public class FlowFieldController : MonoBehaviour
    {
        public int gridSizeX = 10;
        public int gridSizeY = 10;
        public float cellSize = 1f;
        public Vector2 gridOrigin = Vector2.zero; // Origin for the flow field grid
        private Vector2[,] flowField;

        [SerializeField] private bool isGizmosRequried;

        // Reference to the player
        [SerializeField] private Transform player;

        void Start()
        {
            InitializeFlowField();
        }

        private void InitializeFlowField()
        {
            // Initialize the flow field array with the correct size and default values
            flowField = new Vector2[gridSizeX, gridSizeY];
            for (int x = 0; x < gridSizeX; x++)
            {
                for (int y = 0; y < gridSizeY; y++)
                {
                    flowField[x, y] = Vector2.zero;
                }
            }
        }

        void OnDrawGizmos()
        {
            if (!isGizmosRequried) return;

            // Avoid null or incorrect grid size
            if (flowField == null || flowField.GetLength(0) != gridSizeX || flowField.GetLength(1) != gridSizeY)
            {
                InitializeFlowField();
            }

            // Draw each cell and its flow direction if the flowField is initialized
            Gizmos.color = Color.yellow;
            for (int x = 0; x < gridSizeX; x++)
            {
                for (int y = 0; y < gridSizeY; y++)
                {
                    // Compute the world position of the cell center based on grid origin
                    Vector2 cellCenter = gridOrigin + new Vector2(x * cellSize + cellSize / 2, y * cellSize + cellSize / 2);

                    // Draw the cell border
                    Gizmos.DrawWireCube((Vector3)cellCenter, new Vector3(cellSize, cellSize, 0));

                    // Draw flow direction vector
                    Vector2 flowDirection = flowField[x, y];
                    Gizmos.color = Color.green;
                    Gizmos.DrawLine((Vector3)cellCenter, (Vector3)(cellCenter + flowDirection * cellSize));
                }
            }
        }

        void Update()
        {
            // If the player is assigned, update the target point to the player's position
            if (player != null)
            {
                Vector2 playerPosition = new Vector2(player.position.x, player.position.y);
                CalculateFlowField(playerPosition);
            }
        }

        private void CalculateFlowField(Vector2 target)
        {
            for (int x = 0; x < gridSizeX; x++)
            {
                for (int y = 0; y < gridSizeY; y++)
                {
                    Vector2 cellCenter = gridOrigin + new Vector2(x * cellSize + cellSize / 2, y * cellSize + cellSize / 2);
                    Vector2 direction = (target - cellCenter).normalized;
                    flowField[x, y] = direction;
                }
            }
        }

        public Vector2 GetFlowDirection(Vector2 position)
        {
            // Convert world position to grid coordinates relative to gridOrigin
            int x = Mathf.FloorToInt((position.x - gridOrigin.x) / cellSize);
            int y = Mathf.FloorToInt((position.y - gridOrigin.y) / cellSize);

            // Ensure the coordinates are within the grid bounds
            if (x >= 0 && x < gridSizeX && y >= 0 && y < gridSizeY)
            {
                return flowField[x, y];
            }
            return Vector2.zero; // Return zero if position is out of bounds
        }
    }
}
