using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyController : MonoBehaviour
{
    public float speed = 1;
    public int health = 3;
    private string playerFloor;
    public string floor;

    private Rigidbody rb;
    public Transform target;
    public EnemyScriptableObject enemyScriptableObject;
    public GameObject gravityInversionField;
    private GameObject gravityInversionFieldParent;

    private bool gravityField = false;

    // Start is called before the first frame update
    void Start()
    {
        rb = GetComponent<Rigidbody>();

        target = GameObject.Find("Player").transform;
        playerFloor = target.GetComponent<PlayerController>().floor.tag;


        gravityInversionFieldParent = GameObject.Find("Gravity Inversion Field Parent");

        CheckGround();

        StartCoroutine("Wait");
    }

    private void Update()
    {
        playerFloor = target.GetComponent<PlayerController>().floor.tag;
    }

    void CheckGround()
    {
        RaycastHit groundHit;
        Ray groundRay = new Ray(rb.position, Vector3.down);
        Physics.Raycast(groundRay, out groundHit, 2f);
        floor = groundHit.transform.tag;
    }

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

    IEnumerator ChaseTarget()
    {
        yield return new WaitForSeconds(1f);

        while (Vector3.Distance(rb.position, target.transform.position) >= enemyScriptableObject.targetDistance)
        {
            transform.LookAt(target.transform.position); 
            Vector3 movePosition = Vector3.Lerp(rb.position, target.transform.position, speed * Time.deltaTime);

            rb.MovePosition(movePosition);

            yield return null;
        }

        print("Acquired Target Lock On!");

        StartCoroutine("CastGravityInversionField");

        yield break;
    }

    IEnumerator CastGravityInversionField()
    {
        yield return new WaitForSeconds(1f);

        while (Vector3.Distance(rb.position, target.transform.position) <= enemyScriptableObject.targetDistance)
        {
            if (!gravityField)
            {
                print("Casting Gravity Inversion field!");
                var targetPos = target.transform.position;
                transform.LookAt(targetPos);
                Instantiate(gravityInversionField, new Vector3(targetPos.x, rb.position.y + 3, targetPos.z), target.transform.rotation, gravityInversionFieldParent.transform);

                gravityField = true;

                yield return new WaitForSeconds(enemyScriptableObject.specialAttackDuration);
            }
            else
            {
                gravityField = false;
            }

            yield return null;

        }

        print("No longer in range, proceeding to chase target!");

        StartCoroutine("ChaseTarget");

        yield break;

    }

    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.CompareTag("Bullet"))
        {
            health -= 1;

            Destroy(collision.gameObject);
            Debug.Log(health);
            if (health <= 0)
            {
                Die();
            }
        }
    }

    private void Die()
    {
        Destroy(gameObject);
    }
}
