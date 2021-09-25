using System.Collections;
using System.Collections.Generic;
using UnityEngine;


[CreateAssetMenu(fileName = "BulletScriptableObject", menuName = "Player/Bullet")]
public class BulletScriptableObject : ScriptableObject
{
    public float maxBullets = 3f;
    public float bulletLifeTime = 3f;
}
