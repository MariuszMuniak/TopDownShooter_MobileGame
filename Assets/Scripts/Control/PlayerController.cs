using System.Collections;
using System.Collections.Generic;
using TDS_MG.Attributes;
using TDS_MG.Combat;
using TDS_MG.Movement;
using UnityEngine;

namespace TDS_MG.Control
{
    public class PlayerController : MonoBehaviour
    {
        PlayerMover mover;
        PlayerFighter fighter;

        private bool isDead;

        public bool IsDead
        {
            get { return isDead; }
        }

        private void Awake()
        {
            mover = GetComponent<PlayerMover>();
            fighter = GetComponent<PlayerFighter>();
        }

        private void Start()
        {
            SetUpDeathEvent();
        }

        public void ActivateBehaviours()
        {
            mover.enabled = true;
            fighter.enabled = true;
        }

        public void DisableBehaviours()
        {
            mover.enabled = false;
            fighter.enabled = false;
        }

        private void SetUpDeathEvent()
        {
            Health health = GetComponent<Health>();

            health.OnDeath.AddListener(() => DisableBehaviours());
            health.OnDeath.AddListener(() => GetComponent<CharacterController>().enabled = false);
            health.OnDeath.AddListener(() => isDead = true);
        }
    }
}
