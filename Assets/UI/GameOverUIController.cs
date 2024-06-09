using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UIElements;

public class GameOverUIController : MonoBehaviour
{
    [SerializeField] private UIDocument uiDocument;

    private GameManager gameManager;
    
    private VisualElement root;
    // Start is called before the first frame update
    void Start()
    {
        gameManager = GameManager.instance;
        gameManager.gameOverUIController = this;
        root = uiDocument.rootVisualElement;
        var menuBtn = root.Q<Button>("backMenu");
        menuBtn.clicked += BackMenu;
        
        root.visible = false;
    }

    void BackMenu()
    {
        SceneManager.LoadScene(0);
    }
    
    public void SetVisibility(bool visible)
    {
        root.visible = visible;
    }
}
