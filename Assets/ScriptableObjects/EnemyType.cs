using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "Data", menuName = "Enemy/EnemyType", order = 1)]
public class EnemyType : ScriptableObject
{
    public Material material;
    public float speed;
    public int maxHealth;
    public int points;
}
