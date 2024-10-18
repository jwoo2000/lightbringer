using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UISounds : MonoBehaviour
{
    [SerializeField] Transform audioListenerTransform;
    [SerializeField] AudioClip selectSound;
    [SerializeField] AudioClip weaponUpgradeSound;
    [SerializeField] AudioClip selectUpgradeSound;

    public void playSelectSFX()
    {
        SoundManager.instance.playSound(SoundManager.AudioType.UI, selectSound, audioListenerTransform.position, 0.4f);
    }

    public void playWeaponUpgradeSFX()
    {
        SoundManager.instance.playSound(SoundManager.AudioType.UI, weaponUpgradeSound, audioListenerTransform.position, 0.3f);
    }

    public void selectUpgradeSFX()
    {
        SoundManager.instance.playSound(SoundManager.AudioType.UI, selectUpgradeSound, audioListenerTransform.position, 0.5f);
    }
}
