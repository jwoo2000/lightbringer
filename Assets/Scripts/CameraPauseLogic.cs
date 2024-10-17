using Cinemachine;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraPauseLogic : MonoBehaviour
{
    [SerializeField]
    CinemachineBrain brain;

    private void Start()
    {
        GetComponent<Camera>().depthTextureMode = DepthTextureMode.Depth;
    }
    void Update()
    {
        if (Time.timeScale != 0f)
        {
            // if playing
            brain.m_UpdateMethod = CinemachineBrain.UpdateMethod.SmartUpdate;
        } else
        {
            // if paused
            brain.m_UpdateMethod = CinemachineBrain.UpdateMethod.ManualUpdate;
        }
    }
}
