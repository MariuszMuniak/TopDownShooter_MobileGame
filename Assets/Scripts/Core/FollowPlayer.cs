using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace TDS_MG.Core
{
    public class FollowPlayer : MonoBehaviour
    {
        Transform player = null;

        private void Awake()
        {
            player = GameObject.FindWithTag("Player").transform;
        }

        private void LateUpdate()
        {
            if (player == null) { return; }

            transform.position = player.position;
        }
    }
}