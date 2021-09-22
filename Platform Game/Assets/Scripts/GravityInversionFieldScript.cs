using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GravityInversionFieldScript : MonoBehaviour
{

    public EnemyScript enemyScript;
    public float force = 20;

    private void Start()
    {
        Destroy(gameObject, enemyScript.specialAttackDuration);
    }

    private void OnTriggerStay(Collider other)
    {
        other.GetComponent<Rigidbody>().AddForce(Vector3.up * force, ForceMode.Force);
    }
}