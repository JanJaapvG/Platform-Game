using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlatformYController : MonoBehaviour
{
    private Rigidbody rb;
    private Vector3 startingPos;
    private Vector3 targetPos;

    public float speed = 2;

    public void Start()
    {
        rb = GetComponent<Rigidbody>();

        startingPos = rb.position;
        targetPos = rb.position + new Vector3(0.0f, 5f, 0.0f);

        StartCoroutine("MoveUp");
    }


    IEnumerator MoveUp()
    {
        yield return new WaitForSeconds(1f);

        while (rb.position.y < targetPos.y)
        {
            rb.MovePosition(rb.position + Vector3.up * Time.deltaTime * speed);

            yield return null;
        }

        StartCoroutine("MoveDown");

        yield break;

    }

    IEnumerator MoveDown()
    {
        yield return new WaitForSeconds(1f);

        while (rb.position.y > startingPos.y)
        {
            rb.MovePosition(rb.position + Vector3.down * Time.deltaTime * speed);

            yield return null;
        }

        StartCoroutine("MoveUp");

        yield break;
    }
}
