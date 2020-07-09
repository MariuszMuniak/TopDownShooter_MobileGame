using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace TDS_MG.Core
{
    public class DestroyAfterTime : MonoBehaviour
    {
        [SerializeField] float lifetime = 5f;

        void Start()
        {
            Destroy(gameObject, lifetime);
        }
    }
}
