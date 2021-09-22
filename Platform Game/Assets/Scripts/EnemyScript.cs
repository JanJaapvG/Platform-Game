using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName ="targetDistanceScript", menuName="EnemyScript/TargetDistance")]
public class EnemyScript : ScriptableObject
{
    public float targetDistance = 15f;
    public float chasingDistance = 100f;
    public int health = 3;
}
