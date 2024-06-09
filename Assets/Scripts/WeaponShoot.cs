using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UIElements;

public class WeaponShoot : MonoBehaviour
{
    [SerializeField] private GameObject bulletPrefab;
    [SerializeField] private GameObject weaponCenter;
    private GameManager gameManager;
    private float interval = .3f;
    private bool canShoot = true;

    void Start()
    {
        gameManager = GameManager.instance;
    }
    
    // Update is called once per frame
    void Update()
    {
        if (Input.GetMouseButton(0)) Shoot();
    }

    void Shoot()
    {
        if (gameManager.gameState != GameState.Play) return;
        if (!canShoot) return;
        Instantiate(bulletPrefab, weaponCenter.transform.position, transform.rotation);
        StartCoroutine(CoolDown());
    }
    
    IEnumerator CoolDown()
    {
        canShoot = false;
        yield return new WaitForSeconds(interval - gameManager.playerUpgrades.bulletIntervalBonus);
        canShoot = true;
    }
}
