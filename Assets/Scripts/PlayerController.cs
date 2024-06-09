using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    GameManager gameManager;

    private int health = 5;
    // Start is called before the first frame update
    void Start()
    {
        gameManager = GameManager.instance;
        gameManager.player = gameObject;
        
        gameManager.gameUIController.UpdateHealth(health);
    }

    private void OnCollisionEnter(Collision other)
    {
        if (!other.gameObject.CompareTag("Enemy")) return;
        Destroy(other.gameObject);
        health--;
        gameManager.gameUIController.UpdateHealth(health);
        if (health <= 0) gameManager.GameOver();
    }
}
