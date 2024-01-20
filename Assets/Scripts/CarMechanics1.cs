using System.Collections;
using System.Collections.Generic;
using System.Numerics;
using UnityEngine;

public class CarMechanics1 : MonoBehaviour
{
    [SerializeField] float acceleration = 4;
    [SerializeField] float maxSpeed;
    [SerializeField] float turnFactor = 2;
    [SerializeField] float driftFactor = .95f;
    Rigidbody2D carRigidbody2D;
    float accelerationInput;
    float steeringInput;
    float rotationAngle;

    void Awake()
    {
        carRigidbody2D = GetComponent<Rigidbody2D>();
    }

    void Update()
    {
        accelerationInput = Input.GetAxis("Vertical");
        steeringInput = Input.GetAxis("Horizontal");
    }

    void FixedUpdate()
    {
        ApplyEngineForce();
        KillLateralVelocity();
        ApplySteering();
    }

    void ApplyEngineForce()
    {

        if (accelerationInput == 0)
        {
            carRigidbody2D.drag = Mathf.Lerp(carRigidbody2D.drag, 2, Time.fixedDeltaTime * 3);
        }

        else
            carRigidbody2D.drag = 0.1f;

        //Caculate how much "forward" we are going in terms of the direction of our velocity

        float velocityVsUp = UnityEngine.Vector2.Dot(transform.up, carRigidbody2D.velocity);

        //Limit so we cannot go faster than the max speed in the "forward" direction 

        if (velocityVsUp > maxSpeed && accelerationInput == 0)
            return;

        //Create a force for the engine
        UnityEngine.Vector2 engineForceVector = acceleration * accelerationInput * transform.up;

        //Apply force and pushes the car forward
        carRigidbody2D.AddForce(engineForceVector, ForceMode2D.Force);
        
    }

    void ApplySteering()
    {

        //Update the rotation angle based on input
        rotationAngle -= steeringInput * turnFactor;

        //Apply steering by rotating the car object
        carRigidbody2D.MoveRotation(rotationAngle);
    }

    void KillLateralVelocity()
    {

        UnityEngine.Vector2 forwardVelocity = transform.up * UnityEngine.Vector2.Dot(carRigidbody2D.velocity, transform.up);
        UnityEngine.Vector2 lateralVelocity = transform.right * UnityEngine.Vector2.Dot(carRigidbody2D.velocity, transform.right);
        carRigidbody2D.velocity = forwardVelocity * lateralVelocity * driftFactor;

    }
}
