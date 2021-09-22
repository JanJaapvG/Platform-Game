using System.Collections;
using System.Collections.Generic;
using UnityEngine;


[CreateAssetMenu(fileName = "BulletScript", menuName = "PlayerScript/BulletCount")]
public class BulletScriptableObject : ScriptableObject
{
    public float maxBullets = 3f;
    public float bulletLifeTime = 3f;
}
