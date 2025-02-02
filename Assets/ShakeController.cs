using System.Collections;
using System.Collections.Generic;
using UnityEngine;
// using UnityEngine.InputSystem;
using UnityEngine.Android;
// using Gyroscope = UnityEngine.InputSystem.Gyroscope;

public class ShakeController : MonoBehaviour
{
    // private InputDevice device;

    // Rigidbody2D MyRb;
    [SerializeField] private float sensitivity = 1f; // Adjust this value to control the sensitivity of the movement
    private Vector3 startPosition;
    private Rigidbody2D rb;
    public Vector3 acceleration;

    bool CanShake;



    private void Start()
    {
        // device = InputSystem.GetDevice<Accelerometer>();
        // if (device == null)
        // {
        //     Debug.LogError("Accelerometer not found.");
        // }
        // else
        // {
        //     InputSystem.EnableDevice(Accelerometer.current);
        //     CanShake = true;
        // }
        // InputSystem.EnableDevice(Gyroscope.current);

        // InputSystem.EnableDevice(AttitudeSensor.current);
        // InputSystem.EnableDevice(GravitySensor.current);

        rb = GetComponent<Rigidbody2D>();
        startPosition = transform.position;
        
    }

    private void FixedUpdate()
    {
        if (CanShake)
        {
            // acceleration = Accelerometer.current.acceleration.ReadValue();
            // Vector3 movement = new Vector3(acceleration.x + rb.velocity.x, acceleration.y + rb.velocity.y, 0f);
            // rb.velocity = movement;
        }
        // Accelerometer.current.acceleration.CheckStateIsAtDefault();
    }

    public void ResetPosition()
    {
        transform.position = startPosition;
        rb.linearVelocity = Vector3.zero;
    }
}


