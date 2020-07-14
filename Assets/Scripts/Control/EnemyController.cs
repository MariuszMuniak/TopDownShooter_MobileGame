using System.Collections;
using System.Collections.Generic;
using TDS_MG.Character;
using TDS_MG.Combat;
using TDS_MG.Movement;
using UnityEngine;

namespace TDS_MG.Control
{
    public class EnemyController : MonoBehaviour
    {
        EnemyMover mover;
        EnemyFighter fighter;
        Transform player;

        private void Awake()
        {
            mover = GetComponent<EnemyMover>();
            fighter = GetComponent<EnemyFighter>();
            player = GameObject.FindWithTag("Player").transform;
        }

        private void Start()
        {
            GetComponent<ZombieSkin>().SetRandomSkin();
        }

        private void Update()
        {
            mover.MoveTo(player.position);
            fighter.Attack();
        }
    }
}
