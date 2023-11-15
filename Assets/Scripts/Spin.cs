using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Spin : MonoBehaviour
{
    public Transform objectToRotate; // Reference to the public transform object
    public float initialRotationSpeed = 10f; // Initial rotation speed
    public float rotationAcceleration = 5f; // Acceleration of rotation speed per second
    public float maxRotationSpeed = 100f; // Maximum rotation speed

    private float currentRotationSpeed; // Current rotation speed

    void Start()
    {
        currentRotationSpeed = initialRotationSpeed;
    }

    void Update()
    {
        // Increase the rotation speed over time
        currentRotationSpeed += rotationAcceleration * Time.deltaTime;

        // Clamp the rotation speed to the maximum value
        currentRotationSpeed = Mathf.Clamp(currentRotationSpeed, 0f, maxRotationSpeed);

        // Rotate the specified object around its Y-axis
        objectToRotate.Rotate(Vector3.up * currentRotationSpeed * Time.deltaTime);
    }
}
