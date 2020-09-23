using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace TDS_MG.Props
{
    public class Rotator : MonoBehaviour
    {
        [SerializeField] Transform target = null;
        [SerializeField] float rotationSpeed = 1f;
        [SerializeField] bool x = false;
        [SerializeField] bool y = false;
        [SerializeField] bool z = false;

        private void LateUpdate()
        {
            Vector3 eulers = new Vector3
            {
                x = x ? 1f : 0f,
                y = y ? 1f : 0f,
                z = z ? 1f : 0f,
            };

            target.Rotate(eulers, rotationSpeed * Time.fixedDeltaTime);
        }
    }
}
