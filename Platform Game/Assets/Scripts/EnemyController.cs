using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyController : MonoBehaviour
{
    public float speed = 1;

    private Rigidbody rb;
    public GameObject target;
    public EnemyScript enemyScript;

    private bool gravityField = false;

    // Start is called before the first frame update
    void Start()
    {
        rb = GetComponent<Rigidbody>();

        StartCoroutine("ChaseTarget");
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

                gravityField = true;

                yield return new WaitForSeconds(3f);
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
}
