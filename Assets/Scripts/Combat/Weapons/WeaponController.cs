using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class WeaponController : MonoBehaviour
{
    [SerializeField]
    private WeaponInfoPanelController infoController;

    [SerializeField]
    private GameObject player;
    [SerializeField]
    private Vector3 weaponOriginOffset = new Vector3(0.0f, 1.0f, 0.0f);

    public const int maxWeaponSlots = 3;
    public List<Weapon> activeWeapons = new List<Weapon>(maxWeaponSlots);
    public List<GameObject> activeWeaponObjects = new List<GameObject>(maxWeaponSlots);
    public List<GameObject> allWeaponObjects = new List<GameObject>();
    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.G))
        {
            equipWeapon(allWeaponObjects[0]);
        }
    }

    public void equipWeapon(GameObject weaponPrefab)
    {
        if (activeWeapons.Count < maxWeaponSlots)
        {
            GameObject weaponInstance = Instantiate(weaponPrefab, player.transform);
            Weapon weapon = weaponInstance.GetComponent<Weapon>();

            if (weapon != null)
            {
                activeWeapons.Add(weapon);
                activeWeaponObjects.Add(weaponInstance);
                weapon.playerTransform = player.transform;
                weapon.weaponOriginOffset = weaponOriginOffset;
                switch (weapon.weaponTier)
                {
                    case Weapon.Tier.Low:
                        if (!infoController.lowPanel.weaponShowing)
                        {
                            infoController.setLowWep(weapon);
                        } else
                        {
                            Debug.LogWarning("low tier weapon ui already has a weapon");
                        }
                        break;
                    case Weapon.Tier.Mid:
                        if (!infoController.midPanel.weaponShowing)
                        {
                            infoController.setMidWep(weapon);
                        } else
                        {
                            Debug.LogWarning("mid tier weapon ui already has a weapon");
                        }
                        break;
                    case Weapon.Tier.High:
                        if (!infoController.highPanel.weaponShowing)
                        {
                            infoController.setHighWep(weapon);
                        } else
                        {
                            Debug.LogWarning("high tier weapon ui already has a weapon");
                        }
                        break;
                    default:
                        Debug.LogWarning("unknown weapon tier, cannot assign weapon to ui info");
                        return;
                }
                Debug.Log($"{weaponInstance.name} equipped.");
            }
            else
            {
                Debug.LogWarning($"weapon script not found on the {weaponPrefab.name}");
            }
        } else
        {
            Debug.LogWarning("cant equip any more weapons");
        }
    }
}
