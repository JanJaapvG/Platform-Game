using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName ="EnemyScriptableObject", menuName="Enemy/Attacks")]
public class EnemyScriptableObject : ScriptableObject
{
    public float targetDistance = 15f;
    public int specialAttackDuration = 3;
}
