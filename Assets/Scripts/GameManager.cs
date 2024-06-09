using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum GameState
{
    Play,
    Pause,
    GameOver
}

public class GameManager : Singleton<GameManager>
{
    public GameObject player;
    public PlayerUpgrades playerUpgrades;
    public GameState gameState = GameState.Play;
    
    public GameUIController gameUIController;
    public GameOverUIController gameOverUIController;

    void Start()
    {
        PauseGame();
    }
    
    public void PauseGame()
    {
        gameState = GameState.Pause;
        Time.timeScale = 0;
    }
    
    public void ResumeGame()
    {
        gameState = GameState.Play;
        Time.timeScale = 1;
    }
    
    public void GameOver()
    {
        gameState = GameState.GameOver;
        Time.timeScale = 0;
        gameOverUIController.SetVisibility(true);
    }
}
