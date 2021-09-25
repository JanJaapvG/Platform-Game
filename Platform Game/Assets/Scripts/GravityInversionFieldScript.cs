using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GravityInversionFieldScript : MonoBehaviour
{

    public EnemyScriptableObject enemyScriptableObject;
    public float force = 10;

    private void Start()
    {
        Destroy(gameObject, enemyScriptableObject.specialAttackDuration);
    }

    private void OnTriggerStay(Collider other)
    {
        other.GetComponent<Rigidbody>().useGravity = false;
        other.GetComponent<Rigidbody>().AddForce(Vector3.up * force, ForceMode.Force);
    }

    private void OnTriggerExit(Collider other)
    {
        other.GetComponent<Rigidbody>().useGravity = true;
    }
}