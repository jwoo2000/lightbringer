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

    public Image StaminaBar;

    // Start is called before the first frame update
    void Start()
    {
        stamina = maxStamina;
        height.position = new Vector3 (0.0f, stamina, 0.0f);
    }

    // Update is called once per frame
    void Update()
    {
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
        height.position = new Vector3 (0.0f, stamina, 0.0f);
        StaminaBar.fillAmount = stamina / maxStamina;
    }
}
