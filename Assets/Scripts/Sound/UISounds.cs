using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UISounds : MonoBehaviour
{
    [SerializeField] Transform mainCameraTransform;
    [SerializeField] AudioClip selectSound;

    public void playSelectSFX()
    {
        SoundManager.instance.playSound(SoundManager.AudioType.UI, selectSound, mainCameraTransform.position, 0.8f);
    }
}
