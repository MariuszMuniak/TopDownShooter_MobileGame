using System.Collections;
using System.Collections.Generic;
using TDS_MG.Movement;
using UnityEngine;

namespace TDS_MG.Control
{
    public class EnemyController : MonoBehaviour
    {
        EnemyMover mover;
        Transform player;

        private void Awake()
        {
            mover = GetComponent<EnemyMover>();
            player = GameObject.FindWithTag("Player").transform;
        }

        private void Update()
        {
            mover.MoveTo(player.position);
        }
    }
}
