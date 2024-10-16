using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DropWepManager : MonoBehaviour
{
    [SerializeField] private Transform playerTransform;
    [SerializeField] private WeaponController playerWeps;
    [SerializeField] private GameObject LowTierFF;
    [SerializeField] private GameObject MidTierFF;
    [SerializeField] private GameObject HighTierFF;

    private const int lowTier = 1;
    private const int midTier = 2;
    private const int highTier = 3;

    private int tierToDrop()
    {
        List<int> droppableTiers = new List<int>();

        if (playerWeps.LowWeapon != null)
        {
            droppableTiers.Add(lowTier);
        }
        if (playerWeps.MidWeapon != null)
        {
            droppableTiers.Add(midTier);
        }
        if (playerWeps.HighWeapon != null)
        {
            droppableTiers.Add(highTier);
        }

        if (droppableTiers.Count > 0)
        {
            int rand = Random.Range(0, droppableTiers.Count);
            Debug.Log("available choices:" + droppableTiers.Count);
            Debug.Log("dropping: " + droppableTiers[rand]);
            return droppableTiers[rand];
        }
        Debug.Log("no drop availble");
        return -1;
    }

    public void triggerDrop(Vector3 location)
    {
        GameObject ffInstance;
        switch (tierToDrop())
        {
            case lowTier:
                ffInstance = Instantiate(LowTierFF, location, Quaternion.identity);
                ffInstance.GetComponent<AbsorbFireflies>().attractor = playerTransform;
                break;
            case midTier:
                ffInstance = Instantiate(MidTierFF, location, Quaternion.identity);
                ffInstance.GetComponent<AbsorbFireflies>().attractor = playerTransform;
                break;
            case highTier:
                ffInstance = Instantiate(HighTierFF, location, Quaternion.identity);
                ffInstance.GetComponent<AbsorbFireflies>().attractor = playerTransform;
                break;
            default:
                Debug.LogWarning("unknown tier to drop, couldnt drop a wep upgrade");
                break;
        }
    }
}
