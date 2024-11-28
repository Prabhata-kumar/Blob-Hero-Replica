using UnityEngine;

public class FlowFieldController : MonoBehaviour
{
    public int gridSizeX = 10;
    public int gridSizeY = 10;
    public float cellSize = 1f;
    public Vector3 gridOrigin = Vector3.zero;
    private Vector2[,] flowField;

    // Target point for agents to follow
    private Vector3 targetPoint;

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
                Vector3 cellCenter = gridOrigin + new Vector3(x * cellSize + cellSize / 2, 0, y * cellSize + cellSize / 2);

                // Draw the cell border
                Gizmos.DrawWireCube(cellCenter, new Vector3(cellSize, 0.1f, cellSize));

                // Draw flow direction vector
                Vector3 flowDirection = new Vector3(flowField[x, y].x, 0, flowField[x, y].y);
                Gizmos.color = Color.green;
                Gizmos.DrawLine(cellCenter, cellCenter + flowDirection * cellSize);
            }
        }
    }

    void Update()
    {
        // Check for mouse click to set a target point
        if (Input.GetMouseButtonDown(0))
        {
            Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
            if (Physics.Raycast(ray, out RaycastHit hit))
            {
                // Update target and recalculate the flow field
                targetPoint = hit.point;
                CalculateFlowField(targetPoint);
            }
        }
    }

    private void CalculateFlowField(Vector3 target)
    {
        for (int x = 0; x < gridSizeX; x++)
        {
            for (int y = 0; y < gridSizeY; y++)
            {
                Vector3 cellCenter = gridOrigin + new Vector3(x * cellSize + cellSize / 2, 0, y * cellSize + cellSize / 2);
                Vector3 direction = (target - cellCenter).normalized;
                flowField[x, y] = new Vector2(direction.x, direction.z);
            }
        }
    }

    public Vector2 GetFlowDirection(Vector3 position)
    {
        // Convert world position to grid coordinates relative to gridOrigin
        int x = Mathf.FloorToInt((position.x - gridOrigin.x) / cellSize);
        int y = Mathf.FloorToInt((position.z - gridOrigin.z) / cellSize);

        // Ensure the coordinates are within the grid bounds
        if (x >= 0 && x < gridSizeX && y >= 0 && y < gridSizeY)
        {
            return flowField[x, y];
        }
        return Vector2.zero; // Return zero if position is out of bounds
    }
}
