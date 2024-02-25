using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyAI : MonoBehaviour
{
    public float maxSpeed = 5f; // Maximum speed of the enemy
    public float acceleration = 2f; // Acceleration rate of the enemy
    public float rotationSpeed = 100f; // Rotation speed of the enemy
    public float driftFactor = 0.9f; // Drift factor to control drifting intensity
    public float carStoppingDistance = 1.5f; // Distance at which the enemy stops chasing the player
    public float avoidanceForce = 5f; // Force applied to avoid obstacles
    public float avoidanceDistance = 2f; // Distance at which to start avoiding obstacles
    public LayerMask obstacleLayer; // Layer mask for obstacles

    private Transform car;
    private Rigidbody2D rb;
    public Transform RayCastPt;
    private float currentSpeed = 0f;
    
    void Start()
    {
        car = GameObject.FindWithTag("Player").transform;
        rb = GetComponent<Rigidbody2D>();
    }

    void FixedUpdate()
    {
        
            if (car != null)
            {
           // Debug.Log(Vector2.Distance(transform.position,GameObject.FindWithTag("Enemy").transform.position));
            Vector2 directionToPlayer = car.position - transform.position;

            // Avoid obstacles
            RaycastHit2D hit = Physics2D.Raycast(RayCastPt.position, directionToPlayer.normalized, avoidanceDistance, obstacleLayer);
            if (hit.collider != null)
            {
                Debug.Log(hit.distance); // Log the name of the obstacle only if it's within avoidance distance

                // Calculate avoidance direction
                Vector2 avoidanceDirection = Vector2.Perpendicular(hit.normal).normalized;

                // Calculate avoidance force based on distance to obstacle
                float avoidanceStrength = Mathf.Clamp01(1f - (hit.distance / avoidanceDistance)); // Strength of avoidance force based on distance
                Vector2 avoidanceForceVector = avoidanceDirection * avoidanceForce * avoidanceStrength;

                // Apply avoidance force to the enemy's velocity
                rb.velocity += avoidanceForceVector * Time.fixedDeltaTime;
            }

            directionToPlayer.Normalize(); // Normalize direction after obstacle avoidance

            // Calculate the angle to rotate towards the player
            float angle = Mathf.Atan2(directionToPlayer.y, directionToPlayer.x) * Mathf.Rad2Deg - 90f;


            // Calculate rotation speed based on drift factor
            float currentRotationSpeed = rotationSpeed * driftFactor;

                // Rotate the enemy towards the player using rb.MoveRotation
                float currentRotation = rb.rotation;
                currentRotation = Mathf.MoveTowardsAngle(currentRotation, angle, currentRotationSpeed * Time.fixedDeltaTime);
                rb.MoveRotation(currentRotation);

            

            // Move the enemy towards the player
            rb.velocity = transform.up * currentSpeed;

            float distance = Vector2.Distance(transform.position, car.position);
                if (distance <= carStoppingDistance)
                {
                    currentSpeed = 0f;
                    return;
                }
            // Apply acceleration to the current speed towards the maximum speed
            currentSpeed = Mathf.MoveTowards(currentSpeed, maxSpeed, acceleration * Time.fixedDeltaTime);


        }
       
    }
}
