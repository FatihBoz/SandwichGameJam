using UnityEngine;

public class CameraControl : MonoBehaviour
{
    // Target to follow (usually the player)
    [Header("Target Settings")]
    [Tooltip("The transform of the object the camera will follow")]
    public Transform target;

    // Camera movement parameters
    [Header("Camera Movement")]
    [Tooltip("How quickly the camera moves to follow the target")]
    [Range(0f, 10f)]
    public float smoothSpeed = 5f;

    [Tooltip("Offset from the target's position")]
    public Vector3 offset = new Vector3(0f, 1f, -10f);

    // Horizontal and vertical bounds
    [Header("Camera Bounds")]
    [Tooltip("Minimum X position the camera can reach")]
    public float minX = float.MinValue;

    [Tooltip("Maximum X position the camera can reach")]
    public float maxX = float.MaxValue;

    [Tooltip("Minimum Y position the camera can reach")]
    public float minY = float.MinValue;

    [Tooltip("Maximum Y position the camera can reach")]
    public float maxY = float.MaxValue;

    // Tracking mode settings
    [Header("Tracking Mode")]
    [Tooltip("If true, camera will only move horizontally")]
    public bool horizontalOnlyTracking = false;

    [Tooltip("If true, camera will only move vertically")]
    public bool verticalOnlyTracking = false;

    // Internal calculation variables
    private Vector3 desiredPosition;
    private Vector3 smoothedPosition;

    private void LateUpdate()
    {
        // Ensure we have a target to follow
        if (target == null)
        {
            Debug.LogWarning("Camera has no target to follow!");
            return;
        }

        // Calculate the desired position with offset
        desiredPosition = target.position + offset;

        // Apply tracking mode restrictions
        if (horizontalOnlyTracking)
        {
            desiredPosition.y = transform.position.y;
        }
        
        if (verticalOnlyTracking)
        {
            desiredPosition.x = transform.position.x;
        }

        // Clamp the position within specified bounds
        desiredPosition.x = Mathf.Clamp(desiredPosition.x, minX, maxX);
        desiredPosition.y = Mathf.Clamp(desiredPosition.y, minY, maxY);

        // Smoothly interpolate between current position and desired position
        smoothedPosition = Vector3.Lerp(transform.position, desiredPosition, smoothSpeed * Time.deltaTime);

        // Update camera position
        transform.position = smoothedPosition;
    }

    // Optional method to set the target dynamically
    public void SetTarget(Transform newTarget)
    {
        target = newTarget;
    }

    // Visualization of camera bounds in the Scene view
    private void OnDrawGizmosSelected()
    {
        // Draw a wire cube to represent camera bounds
        Gizmos.color = Color.yellow;
        Vector3 center = new Vector3((minX + maxX) / 2, (minY + maxY) / 2, 0);
        Vector3 size = new Vector3(
            maxX - minX, 
            maxY - minY, 
            1f
        );
        Gizmos.DrawWireCube(center, size);
    }
}