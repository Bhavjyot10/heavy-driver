using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CarController : MonoBehaviour
{
    // Settings
    public float MoveSpeed = 50f;
    public float MaxSpeed = 15f;
    public float Drag = 0.98f;
    public float SteerAngle = 20f;
    public float Traction = 1f;

    // Variables
    private Vector2 MoveForce;

    // Update is called once per frame
    void Update()
    {
        // Moving
        MoveForce += (Vector2)(transform.up * MoveSpeed * Input.GetAxis("Vertical") * Time.deltaTime);
        transform.position += new Vector3(MoveForce.x, MoveForce.y, 0) * Time.deltaTime;

        // Steering
        float steerInput = Input.GetAxis("Horizontal");
        transform.Rotate(Vector3.back * steerInput * MoveForce.magnitude * SteerAngle * Time.deltaTime);

        // Drag and max speed limit
        MoveForce *= Drag;
        MoveForce = Vector2.ClampMagnitude(MoveForce, MaxSpeed);

        // Traction
        Debug.DrawRay(transform.position, MoveForce.normalized * 3);
        Debug.DrawRay(transform.position, transform.up * 3, Color.blue);
        MoveForce = Vector2.Lerp(MoveForce.normalized, transform.up, Traction * Time.deltaTime) * MoveForce.magnitude;
    }
}
