using System.Collections.Generic;
using UnityEngine;

public class WeaponController : MonoBehaviour
{
    public List<Weapon> activeWeapons = new List<Weapon>();  // Three active weapon slots
    public int maxWeaponSlots = 3;  // Maximum number of weapon slots
    public List<Weapon> inventory = new List<Weapon>();  // List of all picked up weapons

    void Update()
    {
        // Update all active weapons
        foreach (Weapon weapon in activeWeapons)
        {
            if (weapon != null)
            {
                weapon.Update1();  // Update the weapon behavior (e.g., auto fire or other logic)
            }
        }
    }

    // This will automatically handle picking up weapons when colliding with a weapon object
    private void OnTriggerEnter(Collider other)
    {
        Weapon weapon = other.GetComponent<Weapon>();
        if (weapon != null)
        {
            PickUpWeapon(weapon);
            Destroy(other.gameObject);  // Remove the weapon from the ground
        }
    }

    // Method to pick up a weapon and equip it in an available slot
    public void PickUpWeapon(Weapon newWeapon)
    {
        if (activeWeapons.Count < maxWeaponSlots)
        {
            // Add the weapon to an available slot if there's space
            activeWeapons.Add(newWeapon);
            Debug.Log("Picked up and equipped: " + newWeapon.name);
        }
        else
        {
            // Add the weapon to the inventory if all slots are full
            inventory.Add(newWeapon);
            Debug.Log("Picked up: " + newWeapon.name + " and added to inventory");
        }
    }

    // Optional: Method to drop a weapon
    public void DropWeapon(int slotIndex)
    {
        if (slotIndex < activeWeapons.Count && activeWeapons[slotIndex] != null)
        {
            Debug.Log("Dropped weapon: " + activeWeapons[slotIndex].name);
            activeWeapons.RemoveAt(slotIndex);  // Remove the weapon from the active slot
        }
    }
}
