using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlatformXController : MonoBehaviour
{
    private Rigidbody rb;

    public float speed = 15;

    public void Start()
    {
        rb = GetComponent<Rigidbody>();

        if (rb.position.x == 15)
        {
            StartCoroutine("MoveLeft");
        }
        if (rb.position.x == -15)
        {
            StartCoroutine("MoveRight");
        }
    }


    IEnumerator MoveLeft()
    {
        yield return new WaitForSeconds(1f);

        while (rb.position.x > -15)
        {
            rb.MovePosition(rb.position + Vector3.left * Time.deltaTime * speed);

            yield return null;
        }

        StartCoroutine("MoveRight");

        yield break;

    }

    IEnumerator MoveRight()
    {
        yield return new WaitForSeconds(1f);

        while (rb.position.x < 15)
        {
            rb.MovePosition(rb.position + Vector3.right * Time.deltaTime * speed);

            yield return null;
        }

        StartCoroutine("MoveLeft");

        yield break;
    }
}

