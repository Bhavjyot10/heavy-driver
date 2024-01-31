using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyAI : MonoBehaviour
{

    public float moveSpeed = 5f;
    public float rotationSpeed = 5f;
    public float carStoppingDistance = 1.5f;
    public Transform car;
    
    void Start()
    {
        car = GameObject.FindWithTag("Player").transform;
    }
    void Update()
    {
        if (car != null)
        {
            Vector2 direction = car.position - transform.position;


            // Lerp to smoothly rotate towards the player
            transform.up = Vector2.Lerp(transform.up, direction.normalized, rotationSpeed * Time.deltaTime);
            float distance = Vector2.Distance(transform.position, car.position);
            if (distance <= carStoppingDistance)
                return;
            // Move the enemy towards the player
            float step = moveSpeed * Time.deltaTime;
            
            transform.position = Vector2.MoveTowards(transform.position, car.position, step);
            
            
        }
    }
}
