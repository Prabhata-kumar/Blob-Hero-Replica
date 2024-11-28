using UnityEngine;

public class AgentController : MonoBehaviour
{
    public FlowFieldController flowFieldController; // Reference to FlowFieldController
    public float moveSpeed = 5f;
    public float stopThreshold = 0.1f; // Minimum distance to stop at target

    private Vector3 targetPosition;
    private bool hasReachedTarget = false;

    void Start()
    {
        targetPosition = transform.position;
    }

    void Update()
    {
        if (hasReachedTarget) return; // Stop updating if the target is reached

        // Get the flow direction from the FlowFieldController
        Vector2 flowDirection = flowFieldController.GetFlowDirection(transform.position);
        Vector3 direction = new Vector3(flowDirection.x, 0, flowDirection.y);

        // Move the agent in the direction of the flow field
        if (direction != Vector3.zero)
        {
            transform.position += direction * moveSpeed * Time.deltaTime;

            // Check if the agent is close enough to the target
            if (Vector3.Distance(transform.position, targetPosition) <= stopThreshold)
            {
                hasReachedTarget = true; // Stop further movement updates
            }
        }
    }

    public void SetTargetPosition(Vector3 target)
    {
        targetPosition = target;
        hasReachedTarget = false; // Reset the target reach status
    }
}
