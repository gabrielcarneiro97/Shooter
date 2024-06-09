using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UIElements;

public class GameUIController : MonoBehaviour
{
    private GameManager gameManager;
    
    [SerializeField] private UIDocument uiDocument;
    private Label healthLabel;
    private Label pointsLabel;
    private VisualElement root;

    // Start is called before the first frame update
    void Start()
    {
        gameManager = GameManager.instance;
        gameManager.gameUIController = this;
        
        root = uiDocument.rootVisualElement;
        healthLabel = root.Q<Label>("health");
        pointsLabel = root.Q<Label>("points");
    }
    
    public void UpdatePoints(int points)
    {
        pointsLabel.text = "Points: " + points;
    }
    
    public void UpdateHealth(int health)
    {
        healthLabel.text = "Health: " + health;
    }
    
    public void ChangeVisibility(bool visible)
    {
        uiDocument.rootVisualElement.visible = visible;
    }
}
