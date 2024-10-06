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

    [SerializeField]
    private Color staminaBarColour = new Color(0.0f,0.6f,0.0f,1.0f);

    public Image StaminaBarLeft;
    public Image StaminaBarRight;
    public Image HealthBar;
    public Image ExpBar;

    private float staminaRatio;

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

        staminaRatio = stamina / maxStamina;
        float r;
        float g;
        if (staminaRatio >= 0.5f)
        {
            float greenToYellowRatio = (staminaRatio - 0.5f) / 0.5f;
            r = 1.0f - greenToYellowRatio;
            g = 0.6f;
        }
        else
        {
            float yellowToRedRatio = staminaRatio / 0.5f;
            r = 1.0f;
            g = 0.6f*yellowToRedRatio;
        }
        staminaBarColour = new Color(r, g, 0.0f, 1.0f);
        StaminaBarLeft.color = staminaBarColour;
        StaminaBarRight.color = staminaBarColour;
        StaminaBarLeft.fillAmount = staminaRatio;
        StaminaBarRight.fillAmount = staminaRatio;
        HealthBar.fillAmount = health / maxHealth;
        ExpBar.fillAmount = exp / maxExp;
        //Debug.Log(exp/maxExp);


        /*StaminaBar.localScale = new Vector3(0.2f, stamina / maxStamina, 0.2f);
        StaminaBar.position = new Vector3(StaminaBar.position.x, 1 + (stamina / maxStamina) / 2, StaminaBar.position.z); */
    }
}
