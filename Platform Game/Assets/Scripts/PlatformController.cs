using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlatformController : MonoBehaviour
{
    private Rigidbody rb;

    public float speed = 2;

    public void Start()
    {
        rb = GetComponent<Rigidbody>();

        StartCoroutine("MoveUp");
    }


    IEnumerator MoveUp()
    {
        while(rb.position.y < 12)
        {
            rb.MovePosition(rb.position + Vector3.up * Time.deltaTime * speed);

            yield return null;
        }

        print("Reached the platform");

        StartCoroutine("MoveDown");

        yield break;

    }

    IEnumerator MoveDown()
    {
        while(rb.position.y > 0)
        {
            rb.MovePosition(rb.position + Vector3.down * Time.deltaTime * speed);

            yield return null;
        }

        print("Reached the floor");

        StartCoroutine("MoveUp");

        yield break;
    }
}
