using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerController : MonoBehaviour
{
    public float speed = 10; 
    public float rotateSpeed = 3;
    public float bulletSpeed = 3000;
    public Transform floor;

    private Rigidbody rb; 
    public GameObject bullet;
    public GameObject secretFloor;
    public GameObject secretWall;
    public BulletScriptableObject bulletScriptableObject;
    public Transform bulletSpawnPoint;
    private GameObject bulletParent;
    public LayerMask ground;

    private float movementX;
    private float movementY;
    private bool grounded;

    private void Awake()
    {
        rb = GetComponent<Rigidbody>();
        CheckGround();
    }

    // Start is called before the first frame update
    void Start()
    {
        bulletParent = GameObject.Find("Bullet Parent");
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
        CheckGround(); 

        if (grounded)
        {
            rb.AddForce(rb.transform.up * 400);
        } else
        {
            // do nothing
        }
    }

    void OnFire()
    {
        if (bulletParent.transform.childCount < bulletScriptableObject.maxBullets)
        {
            GameObject bulletInstance = Instantiate(bullet, bulletSpawnPoint.position, rb.rotation, bulletParent.transform) as GameObject;
            bulletInstance.GetComponent<Rigidbody>().AddForce(rb.transform.forward * bulletSpeed);
        }
    }

    void CheckGround()
    {
        RaycastHit groundHit;
        Ray groundRay = new Ray(rb.position, Vector3.down);
        grounded = (Physics.Raycast(groundRay, out groundHit, 2f, ground));
        floor = groundHit.transform;

        Debug.Log(floor);
    }

    private void OnCollisionEnter(Collision collision)
    {
        if (collision.collider.CompareTag("Floor 3"))
        {
            //Instantiate(secretFloor);
            //Instantiate(secretWall);
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
