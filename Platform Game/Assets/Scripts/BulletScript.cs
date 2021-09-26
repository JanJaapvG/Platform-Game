using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BulletScript : MonoBehaviour
{
    public BulletScriptableObject bulletScriptableObject;
    public AudioSource bulletShot;

    private void Awake()
    {
        bulletShot = GetComponent<AudioSource>();
    }
    // destroys the bullet when it's lifetime ends
    private void Start()
    {
        bulletShot.Play();
        Destroy(gameObject, bulletScriptableObject.bulletLifeTime);
    }

    // rotates the bullet
    void Update()
    {
        transform.Rotate(new Vector3(15, 30, 45) * Time.deltaTime);
    }

    // destroys the bullet on collision
    private void OnCollisionEnter(Collision collision)
    {
        Destroy(gameObject);
    }
}
