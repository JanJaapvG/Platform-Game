using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyController : MonoBehaviour
{
    public float speed = 2;

    private Rigidbody rb;
    public GameObject target;
    public EnemyScript enemyScript;

    // Start is called before the first frame update
    void Start()
    {
        rb = GetComponent<Rigidbody>();

        StartCoroutine("Chase");
    }

    IEnumerator Chase()
    {
        yield return null;

        while (Vector3.Distance(rb.position, target.transform.position) > enemyScript.targetDistance)
        {
            transform.LookAt(target.transform.position);
            rb.AddRelativeForce(Vector3.forward * speed, ForceMode.Force);

            yield return null;
        }

        print("In Range");

        yield break;
    }
}
