using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class EnemyController : MonoBehaviour
{
    public float speed = 1;
    public int health = 3;
    public float force = 200;
    public float lockOnTime = 0;
    private string playerFloor;
    public string floor;
    private bool lockedOn = false;

    private Rigidbody rb;
    public Transform target;
    public EnemyScriptableObject enemyScriptableObject;
    public UnityEvent LockedOn;
    public UnityEvent NotLockedOn;
    public AudioSource hit;


    // Start is called before the first frame update
    void Start()
    {
        rb = GetComponent<Rigidbody>();

        // get the player as target and check what is his current floor
        target = GameObject.Find("Player").transform;
        playerFloor = target.GetComponent<PlayerController>().floor.tag;

        hit = GetComponent<AudioSource>();
      
        // sets the floor which the enemy is currently on
        CheckGround();

        StartCoroutine("Wait");
    }

    // coroutine that waits untill the target is on the same floor to attack
    IEnumerator Wait()
    {
        yield return new WaitForSeconds(1f);

        while (floor != playerFloor)
        {   
            yield return null;
        }
        
        print("Target in sight!");

        StartCoroutine("ChaseTarget");

        yield break;
    }

    // chases the target untill the targetDistance
    IEnumerator ChaseTarget()
    {
        yield return new WaitForSeconds(1f);

        while (Vector3.Distance(rb.position, target.position) >= enemyScriptableObject.targetDistance)
        {
            transform.LookAt(target.transform.position); 
            Vector3 movePosition = Vector3.Lerp(rb.position, target.transform.position, speed * Time.deltaTime);

            rb.MovePosition(movePosition);

            yield return null;
        }

        print("Acquired Target Lock On!");

        StartCoroutine("LockOnTarget");

        yield break;
    }

    // publish event that the enemy has a Lock on the location of the target.
    // after 3 seconds publish an event that the enemy has lost the Lock on the location of the target.
    IEnumerator LockOnTarget()
    {
        transform.LookAt(target.position);

        while (lockOnTime <= 3)
        {
            LockOn();
            lockOnTime += Time.timeScale;
            rb.AddForce(Vector3.up * force);

            yield return new WaitForSeconds(1f);
        }

        lockOnTime = 0;
        LostLockOn();

        print("Lost Target Lock proceeding to chase target!");

        StartCoroutine("ChaseTarget");

        yield break;

    }


    // publishes an event that the enemy has is locked on to the targets position
    void LockOn()
    {
        if (!lockedOn)
        {
            LockedOn.Invoke();
            lockedOn = true;
        }
    }

    // publishes an event that the enemy has is no longer locked on to the targets position
    void LostLockOn()
    {
        if (lockedOn)
        {
            NotLockedOn.Invoke();
            lockedOn = false;
        }
    }

    // update the targets floor every frame
    // check if this can be done more efficiently later
    private void Update()
    {
        playerFloor = target.GetComponent<PlayerController>().floor.tag;
    }

    // method to set the floor which the enemy is currently on
    void CheckGround()
    {
        RaycastHit groundHit;
        Ray groundRay = new Ray(rb.position, Vector3.down);
        Physics.Raycast(groundRay, out groundHit, 2f);
        floor = groundHit.transform.tag;
    }

    // checks if the enemy gets hit by a bullet and will drop it's health by 1
    // when health is 0, die will be called which destroys the enemy
    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.CompareTag("Bullet"))
        {
            hit.Play();
            health -= 1;

            Destroy(collision.gameObject, 1f);
            if (health <= 0)
            {
                hit.Play();
                Die();
            }
        }
    }

    // method to destroy the enemy gameobject and publish an event that the enemy has lost it's Lock on the target's position
    void Die()
    {
        LostLockOn();
        lockedOn = false;

        Destroy(gameObject);
    }

    //The old method for using the special attack
    //IEnumerator CastSpecialAttack()
    //{
    //    while (Vector3.Distance(rb.position, target.position) =< enemyScriptableObject.targetDistance)
    //    {
    //        if (!gravityField)
    //        {
    //            print("Casting Gravity Inversion field!");

    //            CastGravityInversionField();

    //            yield return new WaitForSeconds(enemyScriptableObject.specialAttackDuration);
    //        }
    //        else
    //        {
    //            gravityField = false;
    //        }
    //        print("Lost Target Lock proceeding to chase target!");

    //        StartCoroutine("ChaseTarget");
    //        yield break;
    //    }
    //}

    //void CastGravityInversionField()
    //{
    //    var targetPos = target.transform.position;
    //    transform.LookAt(targetPos);
    //    Instantiate(gravityInversionField, new Vector3(targetPos.x, rb.position.y + 3, targetPos.z), target.transform.rotation, gravityInversionFieldParent.transform);

    //    gravityField = true;
    //}
}
