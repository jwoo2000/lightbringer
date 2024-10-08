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

    [SerializeField]
    private Weapon LowWeapon = null;

    [SerializeField]
    private Weapon MidWeapon = null;

    [SerializeField]
    private Weapon HighWeapon = null;

    public const int maxWeaponSlots = 3;
    public List<Weapon> activeWeapons = new List<Weapon>(maxWeaponSlots);
    public List<GameObject> lowTierWeaponPrefabs = new List<GameObject>();
    public List<GameObject> midTierWeaponPrefabs = new List<GameObject>();
    public List<GameObject> highTierWeaponPrefabs = new List<GameObject>();
    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.G))
        {
            equipWeapon(lowTierWeaponPrefabs[0]);
        }
        if (Input.GetKeyDown(KeyCode.H))
        {
            equipWeapon(midTierWeaponPrefabs[0]);
        }
        if (Input.GetKeyDown(KeyCode.J))
        {
            equipWeapon(highTierWeaponPrefabs[0]);
        }
    }

    public void absorbedWepFF(Weapon.Tier tier)
    {
        switch (tier)
        {
            case Weapon.Tier.Low:
                break;
            case Weapon.Tier.Mid:
                break;
            case Weapon.Tier.High:
                break;
            default:
                Debug.LogWarning("WeaponController: Unknown tier weapon firefly absorbed");
                return;
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
                weapon.playerTransform = player.transform;
                weapon.weaponOriginOffset = weaponOriginOffset;
                switch (weapon.weaponTier)
                {
                    case Weapon.Tier.Low:
                        if (!infoController.lowPanel.weaponShowing)
                        {
                            LowWeapon = weapon;
                            infoController.setLowWep(weapon);
                        } else
                        {
                            Debug.LogWarning("low tier weapon ui already has a weapon");
                        }
                        break;
                    case Weapon.Tier.Mid:
                        if (!infoController.midPanel.weaponShowing)
                        {
                            MidWeapon = weapon;
                            infoController.setMidWep(weapon);
                        } else
                        {
                            Debug.LogWarning("mid tier weapon ui already has a weapon");
                        }
                        break;
                    case Weapon.Tier.High:
                        if (!infoController.highPanel.weaponShowing)
                        {
                            HighWeapon = weapon;
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
