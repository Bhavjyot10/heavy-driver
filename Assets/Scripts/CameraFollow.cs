using UnityEngine;

public class FollowCar : MonoBehaviour
{
    public Transform target; // Reference to the car's transform
    public Vector3 offset = new Vector3(0f, 5f, -10f); // Offset from the car

    public float smoothSpeed = 5f; // Smoothing factor for camera movement

    void LateUpdate()
    {
        if (target == null)
        {
            Debug.LogWarning("No target set for the camera to follow.");
            return;
        }

        // Calculate the desired position for the camera
        Vector3 desiredPosition = target.position + offset;

        // Smoothly interpolate towards the desired position
        Vector3 smoothedPosition = Vector3.Lerp(transform.position, desiredPosition, smoothSpeed * Time.deltaTime);
        transform.position = smoothedPosition;

        // Look at the car
        transform.LookAt(target);
    }
}
