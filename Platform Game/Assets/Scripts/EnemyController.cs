using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyController : MonoBehaviour
{
    public float speed = 1;
    public int health = 3;

    private Rigidbody rb;
    public GameObject target;
    public EnemyScript enemyScript;
    public GameObject gravityInversionField;
    private GameObject gravityInversionFieldParent;
    private GravityInversionFieldScript gravityInversionFieldScript;

    private bool gravityField = false;

    // Start is called before the first frame update
    void Start()
    {
        rb = GetComponent<Rigidbody>();
        gravityInversionFieldParent = GameObject.Find("Gravity Inversion Field Parent");

        StartCoroutine("Wait");
    }

    IEnumerator Wait()
    {
        yield return new WaitForSeconds(1f);

        while (Vector3.Distance(rb.position, target.transform.position) > enemyScript.chasingDistance)
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

        while (Vector3.Distance(rb.position, target.transform.position) > enemyScript.targetDistance)
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

        while (Vector3.Distance(rb.position, target.transform.position) <= enemyScript.targetDistance)
        {
            if (!gravityField)
            {
                print("Casting Gravity Inversion field!");

                Instantiate(gravityInversionField, target.transform.position, target.transform.rotation, gravityInversionFieldParent.transform);

                gravityField = true;

                yield return new WaitForSeconds(enemyScript.specialAttackDuration);
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
