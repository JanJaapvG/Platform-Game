using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerController : MonoBehaviour
{
    public float speed = 10; 
    public float rotateSpeed = 3;
    public float bulletSpeed = 3000;
    private bool enteredFloor3 = false;
    private float movementX;
    private float movementY;
    private bool grounded;
    private bool aiming = false;

    private Rigidbody rb; 
    public GameObject bullet;
    public Transform floor;
    public Transform bulletSpawnPoint;
    private GameObject bulletParent;
    public BulletScriptableObject bulletScriptableObject;
    public LayerMask ground;
    public GameObject secretFloor;
    public GameObject secretWall;

    private void Awake()
    {
        rb = GetComponent<Rigidbody>();
        CheckGround();
    }

    // Start is called before the first frame update
    void Start()
    {
        //Empty opbject to collect all bullets
        bulletParent = GameObject.Find("Bullet Parent");
    }

    // method to move the player
    void OnMove(InputValue movementValue)
    {
        Vector2 movementVector = movementValue.Get<Vector2>();

        movementX = movementVector.x;
        movementY = movementVector.y;

    }

    // allows the player to aim better with lower rotation speed when pressing shift
    void OnAim()
    {
        if (!aiming)
        {
            rotateSpeed = 1;
            aiming = true;
        } else if (aiming)
        {
            rotateSpeed = 3;
            aiming = false;
        }
    }

    // method to jump if the player is on the ground
    void OnJump()
    {
        CheckGround(); 

        if (grounded)
        {
            rb.AddForce(Vector3.up * 400);
        } else
        {
            print("Can't jump when airborne");
        }
    }

    // method to fire bullets at enemy's
    void OnFire()
    {
        if (bulletParent.transform.childCount < bulletScriptableObject.maxBullets)
        {
            GameObject bulletInstance = Instantiate(bullet, bulletSpawnPoint.position, rb.rotation, bulletParent.transform) as GameObject;
            bulletInstance.GetComponent<Rigidbody>().AddForce(rb.transform.forward * bulletSpeed);
        }
    }

    //method to check if the player is on the ground and sets the floor to his current floor
    void CheckGround()
    {
        RaycastHit groundHit;
        Ray groundRay = new Ray(rb.position, Vector3.down);
        if (Physics.Raycast(groundRay, out groundHit, 2f, ground))
        {
            grounded = true;
            floor = groundHit.transform;
        }
        else
        {
            grounded = false;
        }
    }

    // If there is time, check if this collision can be placed in a script on Floor 3 itself
    private void OnCollisionEnter(Collision collision)
    {
        if (collision.collider.CompareTag("Floor 3") && enteredFloor3 == false)
        {
            Instantiate(secretFloor);
            Instantiate(secretWall);

            enteredFloor3 = true;
        }

        CheckGround();
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
