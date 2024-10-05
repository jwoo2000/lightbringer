using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Stamina : MonoBehaviour
{
    [SerializeField] private GameObject player;

    public float maxStamina = 100.0f; // default 100
    public float regenerationRate = 10.0f; // amt per second
    public float consumptionRate = 20.0f; // amt per second
    public float stamina; // curr stamina
    private float health;
    private float maxHealth;
    private float exp;
    private float maxExp;
    PlayerStats stats;

    public Image StaminaBar;
    public Image HealthBar;
    public Image ExpBar;

    //public Transform StaminaBar;

    // Start is called before the first frame update
    void Awake()
    {
        stats = player.GetComponent<PlayerStats>();
    }

    void Start()
    {
        stamina = maxStamina;
    }

    // Update is called once per frame
    void Update()
    {
        health = GameManager.gameManager._playerHealth.Health;
        maxHealth = GameManager.gameManager._playerHealth.MaxHealth;
        exp = stats.Exp;
        maxExp = stats.MaxExp;
        if (Input.GetKey(KeyCode.LeftShift) && stamina > 0) {
            stamina -= consumptionRate*Time.deltaTime;
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
        StaminaBar.fillAmount = stamina / maxStamina;
        HealthBar.fillAmount = health / maxHealth;
        ExpBar.fillAmount = exp / maxExp;
        //Debug.Log(exp/maxExp);


        /*StaminaBar.localScale = new Vector3(0.2f, stamina / maxStamina, 0.2f);
        StaminaBar.position = new Vector3(StaminaBar.position.x, 1 + (stamina / maxStamina) / 2, StaminaBar.position.z); */
    }
}
