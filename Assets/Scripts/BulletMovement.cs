using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BulletMovement : MonoBehaviour
{
    [SerializeField] public BulletType bulletData;
    private GameManager gameManager;
    private Rigidbody rb;

    void Start()
    {
        gameManager = GameManager.instance;
        rb = GetComponent<Rigidbody>();
    }
    
    void Update()
    {
        Move();
    }
    
    void Move()
    {
        rb.velocity = transform.forward * (bulletData.speed + gameManager.playerUpgrades.bulletSpeedBonus);
    }
}
