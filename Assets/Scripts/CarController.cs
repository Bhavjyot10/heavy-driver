using Cinemachine;
using System.Collections;
using System.Collections.Generic;
using Unity.Mathematics;
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
    private bool StartCar;
    private float originalFrequency = 1f; // Store the original noise amplitude

    public CinemachineVirtualCamera VirtualCamera;
    public float orthoSizeIncreaseSpeed = 2f;
    public float targetOrthoSize = 20f;

    private void Start()
    {
        StartCar = false;
        //originalFrequency = VirtualCamera.GetCinemachineComponent<CinemachineBasicMultiChannelPerlin>().m_FrequencyGain;
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.anyKeyDown)
        {
            StartCar = true;
        }

        if (StartCar)
        {
            DollyCamera();

            // Moving
            MoveForce += (Vector2)(transform.up * MoveSpeed * Time.deltaTime);
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
    void DollyCamera()
    {
        VirtualCamera.m_Lens.OrthographicSize = Mathf.MoveTowards(VirtualCamera.m_Lens.OrthographicSize, targetOrthoSize, orthoSizeIncreaseSpeed * Time.deltaTime);


        CinemachineBasicMultiChannelPerlin noise = VirtualCamera.GetCinemachineComponent<CinemachineBasicMultiChannelPerlin>();

        // Shake effect when the car starts moving
        float shakeDuration = 5f; // Adjust the duration of the shake effect
        float shakeAmplitude = 20f; // Adjust the amplitude of the shake effect

        if (Time.timeSinceLevelLoad < shakeDuration)
        {
            noise.m_FrequencyGain = Mathf.Lerp(originalFrequency, shakeAmplitude, Time.timeSinceLevelLoad / shakeDuration);
        }
        else
        {
            noise.m_FrequencyGain = originalFrequency;
        }
    }
}

