using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    [SerializeField] private FloatingJoystick _floatingJoystick;
	
    [SerializeField] private float 
	    minMovementSpeed = 0, maxMovementSpeed = 10f, 
	    accelerationRate = 2.5f, 
	    horizontalSpeed = 5.0f;

    [HideInInspector] public float movementSpeed = 0;
    private float horizontalAxis = 0;

    private PlayerAnimation _playerAnimation;
    [HideInInspector] public bool isMovementAvailable;

    private void Start()
    {
        _playerAnimation = GetComponent<PlayerAnimation>();
        isMovementAvailable = true;
    }

    private void FixedUpdate()
    {
        if (isMovementAvailable)
        {
            Movement();
        }
    }
    
    private void Movement()
    {
        
        // if ((Input.touchCount == 1 || Input.GetMouseButton(0)) || Input.GetKey(KeyCode.W))
        // {
        //     movementSpeed += movementSpeed < maxMovementSpeed ? Time.deltaTime*accelerationRate : 0;
        //     horizontalAxis = Input.GetKey(KeyCode.W) ? Input.GetAxis("Horizontal") : _floatingJoystick.Horizontal;
        // }
        // else
        // {
        //     movementSpeed -= movementSpeed > minMovementSpeed ? Time.deltaTime*accelerationRate : 0;
        //     horizontalAxis = 0;
        // }
        //
        // transform.Translate(
        //     new Vector3(
	       //      horizontalAxis*horizontalSpeed, 
        //         0, 
        //         movementSpeed
        //     )*Time.deltaTime
        // );
        
        
        if ((Input.touchCount == 1 || Input.GetMouseButton(0)) || Input.GetKey(KeyCode.W))
        {
            movementSpeed += movementSpeed < maxMovementSpeed ? Time.fixedDeltaTime*accelerationRate : 0;
            horizontalAxis = Input.GetKey(KeyCode.W) ? Input.GetAxis("Horizontal") : _floatingJoystick.Horizontal;
        }
        else
        {
            movementSpeed -= movementSpeed > minMovementSpeed ? Time.fixedDeltaTime*accelerationRate : 0;
            horizontalAxis = 0;
        }

        Vector3 forwardMove = transform.forward * movementSpeed * Time.fixedDeltaTime;
        Vector3 horizontalMove = transform.right * horizontalAxis * horizontalSpeed * Time.fixedDeltaTime;
        GetComponent<Rigidbody>().MovePosition(transform.position + forwardMove + horizontalMove);
        
        _playerAnimation.PlayerMovementAnimationBySpeed(movementSpeed);
    }
}