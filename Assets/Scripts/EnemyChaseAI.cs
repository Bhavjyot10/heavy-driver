using UnityEngine;

public class EnemyChaseAI : MonoBehaviour
{
    public float chaseSpeed = 5f;
    public float rotationSpeed = 2f;
    public float traction = 1f;
    public float drag = 0.98f;

    private Transform player;
    private Vector2 moveForce;

    void Start()
    {
        player = GameObject.FindGameObjectWithTag("Player").transform;
    }

    void Update()
    {
        if (player != null)
        {
            // Calculate direction to the player
            Vector3 direction = player.position - transform.position;
            direction.Normalize();

            // Rotate towards the player
            Quaternion toRotation = Quaternion.LookRotation(Vector3.forward, direction);
            transform.rotation = Quaternion.Lerp(transform.rotation, toRotation, rotationSpeed * Time.deltaTime);

            // Move towards the player
            MoveTowardsPlayer(chaseSpeed);

            // Apply drag
            moveForce *= drag;
        }
    }

    void MoveTowardsPlayer(float speed)
    {
        // Move in the forward direction of the enemy
        moveForce += (Vector2)(transform.up * speed * Time.deltaTime);
        transform.position += new Vector3(moveForce.x, moveForce.y, 0) * Time.deltaTime;

        // Traction - adjust the enemy's direction gradually towards the desired direction
        Vector2 desiredDirection = (player.position - transform.position).normalized;
        transform.up = Vector2.Lerp(transform.up, desiredDirection, traction * Time.deltaTime);
    }
}
