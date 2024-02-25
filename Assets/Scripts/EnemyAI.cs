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
    public float repulsionRadius = 2f; // Radius within which enemies repel each other
    public float repulsionForce = 10f; // Force with which enemies repel each other


    void Start()
    {
        car = GameObject.FindWithTag("Player").transform;
        rb = GetComponent<Rigidbody2D>();
    }

    void FixedUpdate()
    {
        
            if (car != null)
            {

            AvoidOverlap();
           // Debug.Log(Vector2.Distance(transform.position,GameObject.FindWithTag("Enemy").transform.position));
            Vector2 directionToPlayer = car.position - transform.position;

            

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

    void AvoidOverlap()
    {
        Collider2D[] nearbyEnemies = Physics2D.OverlapCircleAll(transform.position, repulsionRadius);
        foreach (Collider2D enemyCollider in nearbyEnemies)
        {
            if (enemyCollider.gameObject != gameObject && enemyCollider.CompareTag("Enemy"))
            {
                Vector3 repulsionDirection = transform.position - enemyCollider.transform.position;
                Rigidbody2D enemyRigidbody = enemyCollider.GetComponent<Rigidbody2D>();

                if (enemyRigidbody != null)
                {
                    enemyRigidbody.AddForce(repulsionDirection.normalized * repulsionForce, ForceMode2D.Force);
                }
            }
        }
    }
}
