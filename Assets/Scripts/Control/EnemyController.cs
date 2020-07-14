using System.Collections;
using System.Collections.Generic;
using TDS_MG.Attributes;
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
        PlayerController playerController;

        private void Awake()
        {
            mover = GetComponent<EnemyMover>();
            fighter = GetComponent<EnemyFighter>();
            player = GameObject.FindWithTag("Player").transform;
            playerController = player.gameObject.GetComponent<PlayerController>();
        }

        private void Start()
        {
            GetComponent<ZombieSkin>().SetRandomSkin();
            SetUpDeathEvent();
        }

        private void Update()
        {
            if (playerController.IsDead) { return; }

            mover.MoveTo(player.position);
            fighter.Attack();
        }

        private void SetUpDeathEvent()
        {
            Health health = GetComponent<Health>();

            health.OnDeath.AddListener(() => GetComponent<CapsuleCollider>().enabled = false);
            health.OnDeath.AddListener(() => mover.StopAgent());
            health.OnDeath.AddListener(() => fighter.enabled = false);
        }
    }
}
