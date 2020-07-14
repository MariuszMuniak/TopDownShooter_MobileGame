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
        private bool isDead;

        public bool IsDead
        {
            get { return isDead; }
        }


        void Start()
        {
            SetUpDeathEvent();
        }

        private void SetUpDeathEvent()
        {
            Health health = GetComponent<Health>();

            health.OnDeath.AddListener(() => GetComponent<PlayerMover>().enabled = false);
            health.OnDeath.AddListener(() => GetComponent<PlayerFighter>().enabled = false);
            health.OnDeath.AddListener(() => GetComponent<CharacterController>().enabled = false);
            health.OnDeath.AddListener(() => isDead = true);
        }
    }
}
