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
    [SerializeField] private MovementController playerMovementController;
    [SerializeField] public PlayerStats _playerStats;

    [SerializeField] private bool playerAlive;

    [SerializeField] private GameObject gameOverCanvas;

    public UnitHealth _playerHealth;

    void Update()
    {
        if ((_playerHealth.Health <= 0.0f) && playerAlive) 
        {
            playerDeath();
        }
    }

    public static void playerWin()
    {
        Debug.Log("Player win!");
    }

    private void playerDeath()
    {
        playerAlive = false;
        Instantiate(playerDeathParticle, player.transform.position, Quaternion.identity);
        playerRB.isKinematic = true;
        playerMesh.enabled = false;
        playerMovementController.controlsActive = false;
        gameOverCanvas.SetActive(true);
    }

    void Awake()
    {
        playerAlive = true;
        gameOverCanvas.SetActive(false);

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
