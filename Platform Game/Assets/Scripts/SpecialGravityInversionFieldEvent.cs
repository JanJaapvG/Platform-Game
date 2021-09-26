using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

[CreateAssetMenu(fileName = "SpecialGravityInversionFieldEvent", menuName = "Enemy/SpecialAttack")]
public class SpecialGravityInversionFieldEvent : ScriptableObject
{
    public int enemiesTargetLocked;
    public int enemiesNeeded = 3;
    public float cooldown;
    private bool specialAttack = false;

    public GameObject target;
    public EnemyScriptableObject enemyScriptableObject;
    public GameObject gravityInversionField;
    private GameObject gravityInversionFieldParent; 

    //sets the target for the special attack
    public void OnEnable()
    {
        enemiesTargetLocked = 0;
        target = GameObject.Find("Player");
        gravityInversionFieldParent = GameObject.Find("Gravity Inversion Field Parent");
    }

    // adds an enemy to the locked enemies
    public void LockedOn()
    {
        enemiesTargetLocked++;
        Debug.Log(enemiesTargetLocked);
        if (enemiesTargetLocked >= enemiesNeeded)
        {
            CastGravityInversionField();
        }
    }

    // removes an enemy from the locked enemies
    public void LostLockOn()
    {
        enemiesTargetLocked--;

        Debug.Log(enemiesTargetLocked);
    }

    // casts a gravityInversionField at the targets position
    void CastGravityInversionField()
    {
        var targetPos = target.transform.position;
        Instantiate(gravityInversionField, new Vector3(targetPos.x, targetPos.y + 3f, targetPos.z), target.transform.rotation, gravityInversionFieldParent.transform);
    }
}
