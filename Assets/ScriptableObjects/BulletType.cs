using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "Data", menuName = "Bullet/BulletType", order = 1)]
public class BulletType : ScriptableObject
{
    public int damage;
    public float speed;
}