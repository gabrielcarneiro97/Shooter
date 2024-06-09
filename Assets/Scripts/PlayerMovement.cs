using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    private GameManager gameManager;
    [SerializeField] private CharacterController controller;
    private float playerSpeed = 15.0f;

    private void Start()
    {
        gameManager = GameManager.instance;
    }

    void Update()
    {
        Move();
    }

    void Move()
    {
        if (gameManager.gameState != GameState.Play) return;
        
        Vector3 move = new Vector3(Input.GetAxis("Horizontal"), 0, Input.GetAxis("Vertical"));
        
        if (!Mathf.Approximately(move.sqrMagnitude, 0) ){
            var pos = move;
            move = transform.TransformDirection(move);
        }
        
        controller.Move(move * (playerSpeed * Time.deltaTime));
    }
    

}
