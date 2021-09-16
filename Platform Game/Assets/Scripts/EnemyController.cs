using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyController : MonoBehaviour
{
    public float speed = 1;

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
        yield return new WaitForSeconds(1f);

        while (Vector3.Distance(rb.position, target.transform.position) > enemyScript.targetDistance)
        {
            transform.LookAt(target.transform.position); 
            Vector3 movePosition = Vector3.Lerp(rb.position, target.transform.position, speed * Time.deltaTime);

            rb.MovePosition(movePosition);

            yield return null;
        }

        print("In Range");

        yield break;
    }
}
