using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerController : MonoBehaviour
{
    public float speed = 10; 
    public float rotateSpeed = 5;

    private Rigidbody rb;

    private float movementX;
    private float movementY;
    private bool grounded;

    // Start is called before the first frame update
    void Start()
    {
        rb = GetComponent<Rigidbody>();
    }

    void OnMove(InputValue movementValue)
    {
        Vector2 movementVector = movementValue.Get<Vector2>();

        movementX = movementVector.x;
        movementY = movementVector.y;

        Debug.Log(movementVector);

    }

    void OnJump()
    {
        checkGround(); 

        if (grounded)
        {
            rb.AddForce(rb.transform.up * 400);
        } else
        {
            // do nothing
        }
    }

    void checkGround()
    {
        RaycastHit groundHit;
        Ray groundRay = new Ray(rb.position, Vector3.down);
        if (Physics.Raycast(groundRay, out groundHit, 1.5f))
        {
            grounded = true;
        } else
        {
            grounded = false;
        }
    }

    void FixedUpdate()
    {
        // Move the player
        Vector3 movement = new Vector3(0.0f, 0.0f, movementY) * Time.deltaTime * speed; 
        movement = transform.TransformDirection(movement);
        rb.MovePosition(transform.position + movement);

        // Rotate the player
        Quaternion deltaRotation = Quaternion.Euler(0.0f, movementX * rotateSpeed, 0.0f);
        rb.MoveRotation(rb.rotation * deltaRotation);
    }
}
