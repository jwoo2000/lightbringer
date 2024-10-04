using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{
    public static GameManager gameManager { get; private set; }

    public UnitHealth _playerHealth = new UnitHealth(100, 100);

    void Update()
    {
        if (_playerHealth.Health <= 0) 
        {
            SceneManager.LoadScene("GameOver");
        }
    }


    void Awake()
    {
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
