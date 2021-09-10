using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerController : MonoBehaviour
{
    public float speed = 10; 
    public float rotateSpeed = 150;

    private Rigidbody rb;
    private float movementX;
    private float movementY;

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

    }

    void OnJump()
    {
        rb.AddForce(rb.transform.up * 400);
    }

    void FixedUpdate()
    {
        // Move the player
        Vector3 movement = new Vector3(movementX, 0.0f, movementY);
        rb.MovePosition(rb.transform.position + movement * Time.deltaTime * speed);

        // Rotate the player, needs to change to MoveRotation
        rb.transform.RotateAround(rb.position, rb.transform.up, movementX * Time.deltaTime * rotateSpeed);
 
    }
}
