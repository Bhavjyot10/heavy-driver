using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BhavCarController : MonoBehaviour
{
    public float maxSpeed = 10f; // Maximum speed of the car
    public float acceleration = 5f; // rate of the car
    public float rotationSpeed = 100f; // Rotation speed of the car
    public float driftFactor = 0.9f; // Drift factor to control drifting intensity

    private Rigidbody2D rb;
    private float currentSpeed = 0f;
    public bool StartCar = false;
    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
    }

    void FixedUpdate()
    {
        if (Input.anyKeyDown)
        {
            StartCar = true;
        }
        if (StartCar)
        {
            float horizontalInput = Input.GetAxis("Horizontal"); // Get horizontal input (A and D keys or arrow keys)

            // Rotate the car based on horizontal input
            float rotation = -horizontalInput * rotationSpeed * Time.fixedDeltaTime;
            rb.MoveRotation(rb.rotation + rotation);

            // Calculate the forward direction based on the current rotation of the car
            Vector2 forwardDirection = transform.up;

            // Apply acceleration to the current speed towards the maximum speed
            currentSpeed = Mathf.MoveTowards(currentSpeed, maxSpeed, acceleration * Time.fixedDeltaTime);

            // Apply drift factor to simulate drifting
            if (Mathf.Abs(horizontalInput) > 0 && currentSpeed > 0)
            {
                // Reduce the rotation speed during drifting
                float driftRotation = rotationSpeed * driftFactor * Time.fixedDeltaTime * -horizontalInput;
                rb.MoveRotation(rb.rotation + driftRotation);
            }

            // Move the car forward continuously in the direction it's facing at the current speed
            rb.velocity = forwardDirection * currentSpeed;
        }
    }
}
