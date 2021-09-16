using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlatformXController : MonoBehaviour
{
    private Rigidbody rb;

    public float speed = 25;

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
        yield return null;

        while (rb.position.x > -15)
        {
            rb.MovePosition(rb.position + Vector3.left * Time.deltaTime * speed);

            yield return null;
        }

        print("I am now Right");

        StartCoroutine("MoveRight");

        yield break;

    }

    IEnumerator MoveRight()
    {
        yield return null;

        while (rb.position.x < 15)
        {
            rb.MovePosition(rb.position + Vector3.right * Time.deltaTime * speed);

            yield return null;
        }

        print("I am now Left");

        StartCoroutine("MoveLeft");

        yield break;
    }
}

