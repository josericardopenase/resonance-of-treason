using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerCamera : MonoBehaviour
{
    public Transform playerTransform;
    public Vector3 offset;
    public float followSpeed = 0.125f;

    private void FixedUpdate()
    {
        FollowPlayer();
    }

    void FollowPlayer()
    {
        // Calculate desired position
        Vector3 desiredPosition = playerTransform.position + offset;

        // Interpolate between the camera's current position and the player's position
        Vector3 smoothedPosition = Vector3.Lerp(transform.position, desiredPosition, followSpeed);

        // Apply the smoothed position to the camera
        transform.position = smoothedPosition;
    }
}
