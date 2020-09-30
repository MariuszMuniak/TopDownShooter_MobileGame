using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace TDS_MG.Core
{
    public class FacingToCamera : MonoBehaviour
    {
        Camera mainCamera;

        private void Awake()
        {
            mainCamera = Camera.main;
        }

        private void Update()
        {
            transform.LookAt(mainCamera.transform);
        }
    }
}
