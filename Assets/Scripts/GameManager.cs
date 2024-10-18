using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{
    public static GameManager gameManager { get; private set; }

    [SerializeField] private GameObject playerDeathParticle;
    [SerializeField] private GameObject player;
    [SerializeField] private Rigidbody playerRB;
    [SerializeField] private SkinnedMeshRenderer playerMesh;
    [SerializeField] public MovementController playerMovementController;
    [SerializeField] public PlayerStats _playerStats;
    [SerializeField] private PlayerSounds playerSounds;
    [SerializeField] private WeaponController playerWeaponController;

    [SerializeField] public static bool playerAlive;

    [SerializeField] private GameObject gameOverCanvas;
    [SerializeField] private GameObject gameWinCanvas;
    [SerializeField] public bool gameWin;

    public UnitHealth _playerHealth;

    void Update()
    {
        if ((_playerHealth.Health <= 0.0f) && playerAlive) 
        {
            playerDeath();
        }
    }

    public void playerWin()
    {
        //Debug.Log("Player win!");
        gameWin = true;
        gameWinCanvas.SetActive(true);
        UnitHealth.calcDamage = false;
    }

    private void playerDeath()
    {
        playerAlive = false;
        _playerStats.expGettable = false;
        Instantiate(playerDeathParticle, player.transform.position, Quaternion.identity);
        playerSounds.playDeath();
        playerRB.isKinematic = true;
        playerMesh.enabled = false;
        foreach (Weapon weapon in playerWeaponController.activeWeapons)
        {
            Destroy(weapon.gameObject);
        }
        playerMovementController.controlsActive = false;
        gameOverCanvas.SetActive(true);
    }

    void Awake()
    {
        playerAlive = true;
        gameOverCanvas.SetActive(false);
        gameWinCanvas.SetActive(false);
        gameWin = false;
        UnitHealth.calcDamage = true;

        if (gameManager != null && gameManager != this)
        {
            Destroy(this);
        }
        else
        {
            gameManager = this;
        }
    }
}
