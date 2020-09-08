using System.Collections;
using System.Collections.Generic;
using TDS_MG.Attributes;
using TDS_MG.Character;
using TDS_MG.Combat;
using TDS_MG.Movement;
using UnityEngine;
using UnityEngine.AI;

namespace TDS_MG.Control
{
    public class EnemyController : MonoBehaviour
    {
        EnemyMover mover;
        EnemyFighter fighter;
        Health health;
        Transform player;
        PlayerController playerController;


        private void Awake()
        {
            mover = GetComponent<EnemyMover>();
            fighter = GetComponent<EnemyFighter>();
            health = GetComponent<Health>();
            player = GameObject.FindWithTag("Player").transform;
            playerController = player.gameObject.GetComponent<PlayerController>();
        }

        private void Start()
        {
            GetComponent<ZombieSkin>().SetRandomSkin();
            SetUpDeathEvent();
            SetUpAnimator();
        }

        private void Update()
        {
            if (health.IsDead() || playerController.IsDead)
            {
                return;
            }

            mover.MoveTo(player.position);
            fighter.Attack();
        }

        private void SetUpDeathEvent()
        {
            Health health = GetComponent<Health>();

            health.OnDeath.AddListener(() => mover.StopAgent());
            health.OnDeath.AddListener(() => fighter.enabled = false);
            health.OnDeath.AddListener(() => GetComponent<CapsuleCollider>().enabled = false);
            health.OnDeath.AddListener(() => GetComponent<NavMeshAgent>().enabled = false);
        }

        private void SetUpAnimator()
        {
            Animator animator = GetComponentInChildren<Animator>();
            animator.SetFloat("CycleOffset", Random.Range(0, 0.1f));
            animator.SetFloat("Locomotion_m", Random.Range(0.85f, 1.15f));
        }
    }
}
