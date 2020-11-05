using System.Collections;
using System.Collections.Generic;
using System.Runtime.CompilerServices;
using UnityEngine;
using UnityEngine.Experimental.UIElements;
using UnityEngine.UI;
using TMPro;

public class PlayerController : MonoBehaviour
{
    // Private Variables
    // [SerializeField] private float speed = 20.0f;
    [SerializeField] private float horsePower = 0;
    [SerializeField] private float turnSpeed = 45.0f;
    [SerializeField] GameObject centerOfMass;

    private float horizontalInput;
    private float verticalInput;
    private Rigidbody playerRb;

    [SerializeField] Text speedometerText;
    [SerializeField] float speed;

    [SerializeField] Text RPMText;
    [SerializeField] float rpm;

    [SerializeField] int wheelsOnGround;
    [SerializeField] List<WheelCollider> allWheels;
    
    const float kph = 3.6f;
    const float mph = 2.327f;


    private void Start()
    {
        playerRb = GetComponent<Rigidbody>();
        playerRb.centerOfMass = centerOfMass.transform.position;
    }

    void FixedUpdate()
    {
        if (IsOnGround())
        {
            playerRb.AddRelativeForce(Vector3.forward * horsePower * verticalInput);
            // Rotates the carr based on horizontal input
            transform.Rotate(Vector3.up, turnSpeed * horizontalInput * Time.deltaTime);

            speed = Mathf.Round(playerRb.velocity.magnitude * kph);
            speedometerText.text = ("Speed: " + speed + " kph");

            rpm = Mathf.Round((speed % 30) * 40);
            RPMText.text = ("RPM: " + rpm);

            horizontalInput = Input.GetAxis("Horizontal");
            verticalInput = Input.GetAxis("Vertical");
        }
    }

    bool IsOnGround()
    {
        wheelsOnGround = 0;
        foreach (WheelCollider wheel in allWheels)
        {
            if (wheel.isGrounded)
            {
                wheelsOnGround++;
            }
        }
        if (wheelsOnGround == 4)
        {
            return true;
        }
        else
        {
            return false;
        }
    }

}
