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

        Debug.Log(rb.position.x);
        if (rb.position.x == 15)
        {
            StartCoroutine("MoveLeft");
        }
        else if (rb.position.x == -15)
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

        print("Right");

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

        print("Left");

        StartCoroutine("MoveLeft");

        yield break;
    }
}

