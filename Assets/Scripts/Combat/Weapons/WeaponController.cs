using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class WeaponController : MonoBehaviour
{
    [SerializeField]
    private GameObject player;

    public const int maxWeaponSlots = 3;
    public List<Weapon> activeWeapons = new List<Weapon>(maxWeaponSlots);
    public List<GameObject> activeWeaponObjects = new List<GameObject>(maxWeaponSlots);
    public List<GameObject> allWeaponObjects = new List<GameObject>();

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
