using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GravityInversionFieldScript : MonoBehaviour
{

    public EnemyScriptableObject enemyScriptableObject;
    public AudioSource cast;
    public float force = 7;

    // destroys the gravityInversionField when it's duration ends
    private void Start()
    {
        cast = GetComponent<AudioSource>();
        Destroy(gameObject, enemyScriptableObject.specialAttackDuration);
    }

    //turns off gravity on the rigidbody that it collides with
    //after it turns off gravity it adds force to the rigidbody
    private void OnTriggerStay(Collider other)
    {
        cast.Play();
        other.GetComponent<Rigidbody>().useGravity = false;
        other.GetComponent<Rigidbody>().AddForce(Vector3.up * force, ForceMode.Force);
    }

    //when the rigidbody exits the gravityinversionfield it's gravity is turned back on
    private void OnTriggerExit(Collider other)
    {
        other.GetComponent<Rigidbody>().useGravity = true;
    }
}