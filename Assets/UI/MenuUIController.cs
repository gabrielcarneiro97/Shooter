using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UIElements;

public class MenuUIController : MonoBehaviour
{
    [SerializeField] private UIDocument uiDocument;
    private VisualElement root;
    private GameManager gameManager;

    void Start()
    {
        gameManager = GameManager.instance;
        root = uiDocument.rootVisualElement;
        
        var playBtn = root.Q<Button>("playBtn");
        playBtn.clicked += StartGame;
        
        var exitBtn = root.Q<Button>("exitBtn");
        exitBtn.clicked += Exit;

    }

    void StartGame()
    {
        gameManager.ResumeGame();
        SceneManager.LoadScene(1);
    }

    void Exit()
    {
        Application.Quit();
    }
}
