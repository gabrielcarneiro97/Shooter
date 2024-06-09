using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy : MonoBehaviour
{
    [SerializeField] private EnemyType[] enemyTypes;
    private EnemyType enemyType;
    private GameManager gameManager;
    private Rigidbody rb;
    private GameObject enemy;
    
    private int health = 0;
    
    // Start is called before the first frame update
    void Start()
    {
        gameManager = GameManager.instance;
        enemyType = enemyTypes[UnityEngine.Random.Range(0, enemyTypes.Length)];
        
        health = enemyType.maxHealth;
        
        GetComponent<Renderer>().material = enemyType.material;
        rb = GetComponent<Rigidbody>();
    }
    
    void Update()
    {
        Move();
    }
    
    void Move()
    {
        transform.LookAt(gameManager.player.transform);
        rb.velocity = transform.forward * (enemyType.speed);
    }

    private void OnTriggerEnter(Collider other)
    {
        if (!other.CompareTag("Bullet")) return;
        health -= other.GetComponent<BulletMovement>().bulletData.damage + gameManager.playerUpgrades.bulletDamageBonus;

        if (!gameManager.playerUpgrades.bulletPierceActive) Destroy(other.gameObject);
        
        
        if (health > 0) return;
        
        gameManager.playerUpgrades.points += enemyType.points;
        gameManager.gameUIController.UpdatePoints(gameManager.playerUpgrades.points);
        Destroy(gameObject);
        
    }
}
