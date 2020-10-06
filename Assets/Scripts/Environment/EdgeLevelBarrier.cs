using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace TDS_MG.Environment
{
    public class EdgeLevelBarrier : MonoBehaviour
    {
        void Start()
        {
            MeshRenderer meshRenderer = GetComponent<MeshRenderer>();

            if (meshRenderer != null)
            {
                meshRenderer.enabled = false;
            }
        }
    }
}
