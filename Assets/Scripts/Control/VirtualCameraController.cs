using Cinemachine;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace TDS_MG.Control
{
    public class VirtualCameraController : MonoBehaviour
    {
        CinemachineVirtualCamera virtualCamera;
        Transform player;

        private void Awake()
        {
            virtualCamera = GetComponentInChildren<CinemachineVirtualCamera>();
            player = GameObject.FindWithTag("Player").transform;
        }

        private void Start()
        {
            if (virtualCamera != null && player != null)
            {
                virtualCamera.LookAt = player;
            }
        }
    }
}
