using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Stamina : MonoBehaviour
{
    public float maxStamina;
    public float regenerationRate;
    private float stamina;
    public Transform height;
    private float health;
    private float maxHealth;

    public Image StaminaBar;
    public Image HealthBar;

    //public Transform StaminaBar;

    // Start is called before the first frame update
    void Start()
    {
        stamina = maxStamina;
        height.position = new Vector3 (0.0f, stamina, 0.0f);
    }

    // Update is called once per frame
    void Update()
    {
        health = GameManager.gameManager._playerHealth.Health;
        maxHealth = GameManager.gameManager._playerHealth.MaxHealth;
        if (Input.GetKey(KeyCode.LeftShift) && stamina > 0) {
            stamina -= Time.deltaTime;
        } else if ((Input.GetKey(KeyCode.LeftShift) && stamina <= 0)) {
            // Stamina will not regerate
        } else {
            if (stamina < maxStamina) {
                stamina += (Time.deltaTime * regenerationRate);
            }
        }
        if (stamina < 0) {
            stamina = 0;
        }
        height.position = new Vector3(0.0f, stamina, 0.0f);
        StaminaBar.fillAmount = stamina / maxStamina;
        HealthBar.fillAmount = health / maxHealth;

        /*StaminaBar.localScale = new Vector3(0.2f, stamina / maxStamina, 0.2f);
        StaminaBar.position = new Vector3(StaminaBar.position.x, 1 + (stamina / maxStamina) / 2, StaminaBar.position.z); */
    }
}
