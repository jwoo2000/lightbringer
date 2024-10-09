using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class HUDLevelsAvailable : MonoBehaviour
{
    [SerializeField]
    private MenuController menuController;

    [SerializeField]
    private TextMeshProUGUI textComponent;

    void Update()
    {
        if (menuController.pendingLevelUps == 1)
        {
            textComponent.text = menuController.pendingLevelUps.ToString() + " level available";
        } else if (menuController.pendingLevelUps > 1)
        {
            textComponent.text = menuController.pendingLevelUps.ToString() + " levels available";
        } else
        {
            textComponent.text = "";
        }
    }
}
