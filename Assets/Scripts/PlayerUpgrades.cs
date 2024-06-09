using System.Collections;
using System.Collections.Generic;
using JetBrains.Annotations;
using UnityEngine;

public class Upgrade
{
    public string name;
    public bool isActive;
    public bool isAvailable;
    public int cost;
    public int treePlacement;

    [CanBeNull] public Upgrade Left;
    [CanBeNull] public Upgrade Right;
    
    public Upgrade(string name, int cost, int treePlacement)
    {
        this.name = name;
        this.cost = cost;
        this.treePlacement = treePlacement;
        isActive = false;
        isAvailable = false;
        Left = null;
        Right = null;
    }
    
    public void Activate()
    {
        if (isAvailable)
        {
            isActive = true;
            if (Left != null) Left.isAvailable = true;
            if (Right != null) Right.isAvailable = true;
        }
    }
}

public class UpgradeTree
{
    public Upgrade root;
    public int Count;
    
    Upgrade Insert(Upgrade upgrade, Upgrade node)
    {
        if (node == null) node = upgrade;
        else if (upgrade.treePlacement > node.treePlacement) node.Right = Insert(upgrade, node.Right);
        else node.Left = Insert(upgrade, node.Left);
        
        return node;
    }
    
    public void Insert(Upgrade upgrade)
    {
        root = Insert(upgrade, root);
        Count += 1;
    }

    public Dictionary<int, List<Upgrade>> GetLevels()
    {
        var levels = new Dictionary<int, List<Upgrade>>();
        GetLevel(levels, root, 0);
        return levels;
    }
    
    void GetLevel(Dictionary<int, List<Upgrade>> levels, Upgrade node, int level)
    {
        if (node == null) return;
        if (!levels.ContainsKey(level)) levels[level] = new List<Upgrade>();
        levels[level].Add(node);
        GetLevel(levels, node.Left, level + 1);
        GetLevel(levels, node.Right, level + 1);
    }
}


public class PlayerUpgrades : MonoBehaviour
{
    private GameManager gameManager;
    public UpgradeTree tree;
    public int points;
    
    public float bulletSpeedBonus = 0f;
    public bool bulletPierceActive = false;
    public int bulletDamageBonus = 0;
    public float bulletIntervalBonus = 0f;
    
    // Start is called before the first frame update
    void Start()
    {
        gameManager = GameManager.instance;
        gameManager.playerUpgrades = this;
        
        tree = new UpgradeTree();
        // 1
        var bulletSpeed = new Upgrade("Bullet Speed", 100, 100)
        {
            isAvailable = true
        };
        tree.Insert(bulletSpeed);
        
        // 1.1
        var bulletDamage = new Upgrade("Bullet Damage",200, 50);
        tree.Insert(bulletDamage);
        // 1.2
        var bulletPierce = new Upgrade("Bullet Pierce",200, 150);
        tree.Insert(bulletPierce);
        
        // 1.1.1
        var bulletSpeed2 = new Upgrade("Bullet Speed",300, 25);
        tree.Insert(bulletSpeed2);
        // 1.1.2
        var bulletInterval = new Upgrade("Bullet Interval",300, 75);
        tree.Insert(bulletInterval);
        
        // 1.2.1
        var bulletInterval2 = new Upgrade("Bullet Interval",300, 125);
        tree.Insert(bulletInterval2);
        // 1.2.2
        var bulletDamage2 = new Upgrade("Bullet Damage",300, 175);
        tree.Insert(bulletDamage2);
    }
    
    public void TryActivateUpgrade(Upgrade upgrade)
    {
        if (points < upgrade.cost) return;
        
        points -= upgrade.cost;
        upgrade.Activate();
        
        gameManager.gameUIController.UpdatePoints(points);
        
        switch (upgrade.name)
        {
            case "Bullet Speed":
                bulletSpeedBonus += 50f;
                break;
            case "Bullet Damage":
                bulletDamageBonus += 1;
                break;
            case "Bullet Interval":
                bulletIntervalBonus += 0.1f;
                break;
            case "Bullet Pierce":
                bulletPierceActive = true;
                break;
        }
    }
    
}
