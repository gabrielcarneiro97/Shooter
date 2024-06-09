using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.UIElements;

public class UpgradesUiController : MonoBehaviour
{
    
    [SerializeField] private UIDocument uiDocument;

    private GameManager gameManager;

    private VisualElement btns;

    private TextElement playerPoints;

    private VisualElement root;
    // Start is called before the first frame update
    void Start()
    {
        gameManager = GameManager.instance;
        root = uiDocument.rootVisualElement;
        btns = root.Q<VisualElement>("buttons");
        playerPoints = root.Q<TextElement>("playerPoints");

        root.visible = false;
    }

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.P))
        {
            if (gameManager.gameState == GameState.Play)
            {
                gameManager.PauseGame();
                gameManager.gameUIController.ChangeVisibility(false);
                CreateUi();
                root.visible = true;
            } else if (gameManager.gameState == GameState.Pause)
            {
                root.visible = false;
                gameManager.gameUIController.ChangeVisibility(true);
                gameManager.ResumeGame();
            }
        }
    }

    void CreateUi()
    {
        playerPoints.text = "Player Points: " + gameManager.playerUpgrades.points;
        if (btns.Children().Any())
        {
            btns.Clear();
        }
        
        var levels = gameManager.playerUpgrades.tree.GetLevels();
        
        foreach (var level in levels)
        {
            btns.Add(CreateLevel(level.Value));
        }
    }

    VisualElement CreateButton(Upgrade upgrade)
    {
        var container = new VisualElement();
        container.AddToClassList("btn-container");
        var button = new Button
        {
            text = upgrade.name + "\n" + upgrade.cost + " Points",
        };
        if (!upgrade.isAvailable || upgrade.isActive) button.SetEnabled(false);
        else
        {
            button.clicked += () =>
            {
                gameManager.playerUpgrades.TryActivateUpgrade(upgrade);
                CreateUi();
            };
        }
        
        button.AddToClassList("upgrade-btn");
        if (upgrade.isActive) button.AddToClassList("active");
        container.Add(button);
        
        return container;
    }

    VisualElement CreateLevel(List<Upgrade> upgrades)
    {
        var level = new VisualElement();
        level.AddToClassList("level");
        foreach (var upgrade in upgrades)
        {
            level.Add(CreateButton(upgrade));
        }
        
        return level;
    }
    
    
}
