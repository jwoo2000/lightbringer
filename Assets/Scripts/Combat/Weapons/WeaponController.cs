using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class WeaponController : MonoBehaviour
{
    [SerializeField]
    private WeaponInfoPanelController infoController;

    [SerializeField]
    private MenuController menuController;

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
    public List<GameObject> lowTierWeaponChoicePanels = new List<GameObject>();
    public List<GameObject> midTierWeaponPrefabs = new List<GameObject>();
    public List<GameObject> midTierWeaponChoicePanels = new List<GameObject>();
    public List<GameObject> highTierWeaponPrefabs = new List<GameObject>();
    public List<GameObject> highTierWeaponChoicePanels = new List<GameObject>();

    public void absorbedWepFF(Weapon.Tier tier)
    {
        switch (tier)
        {
            case Weapon.Tier.Low:
                Debug.Log("Absorbed Low Wep FF");
                if (LowWeapon == null)
                {
                    // first wep ff of tier, open get weapon
                    menuController.newWeaponChoices(tier);
                } else
                {
                    // not first, open upgrade for tier
                }
                break;
            case Weapon.Tier.Mid:
                Debug.Log("Absorbed Mid Wep FF");
                break;
            case Weapon.Tier.High:
                Debug.Log("Absorbed High Wep FF");
                break;
            default:
                Debug.LogWarning("WeaponController: Unknown tier weapon firefly absorbed");
                return;
        }
    }

    public (GameObject, GameObject, GameObject, GameObject, GameObject, GameObject) random3SelectWeapon(Weapon.Tier tier)
    {
        int possibleChoiceCount = getWeaponPrefabs(tier).Count;
        
        //Debug.Log("generating choices");
        int[] possibleChoices = new int[possibleChoiceCount];
        // populate possible index choices
        for (int i = 0; i < possibleChoices.Length; i++)
        {
            possibleChoices[i] = i;
        }
        // fisher-yates shuffle
        for (int i = possibleChoices.Length - 1; i > 0; i--)
        {
            int j = Random.Range(0, i + 1);
            int temp = possibleChoices[i];
            possibleChoices[i] = possibleChoices[j];
            possibleChoices[j] = temp;
        }

        GameObject left = getWeaponPrefabs(tier)[possibleChoices[0]];
        GameObject leftPanel = getWeaponPanelPrefabs(tier)[possibleChoices[0]];
        GameObject middle = getWeaponPrefabs(tier)[possibleChoices[1]];
        GameObject midPanel = getWeaponPanelPrefabs(tier)[possibleChoices[1]];
        GameObject right = getWeaponPrefabs(tier)[possibleChoices[2]];
        GameObject rightPanel = getWeaponPanelPrefabs(tier)[possibleChoices[2]];
        return (left, leftPanel, middle, midPanel, right, rightPanel);
    }

    private List<GameObject> getWeaponPrefabs(Weapon.Tier tier)
    {
        switch (tier)
        {
            case Weapon.Tier.Low:
                return lowTierWeaponPrefabs;
            case Weapon.Tier.Mid:
                return midTierWeaponPrefabs;
            case Weapon.Tier.High:
                return highTierWeaponPrefabs;
            default:
                Debug.LogWarning("WeaponController: Unknown tier to get weapon prefabs list");
                return null;
        }
    }

    private List<GameObject> getWeaponPanelPrefabs(Weapon.Tier tier)
    {
        switch (tier)
        {
            case Weapon.Tier.Low:
                return lowTierWeaponChoicePanels;
            case Weapon.Tier.Mid:
                return midTierWeaponChoicePanels;
            case Weapon.Tier.High:
                return highTierWeaponChoicePanels;
            default:
                Debug.LogWarning("WeaponController: Unknown tier to get weapon choice panels list");
                return null;
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
