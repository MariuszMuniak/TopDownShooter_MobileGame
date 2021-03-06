﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

namespace TDS_MG.Movement
{
    public class EnemyMover : MonoBehaviour
    {
        NavMeshAgent agent;
        Animator animator;

        private void Awake()
        {
            agent = GetComponent<NavMeshAgent>();
            animator = GetComponentInChildren<Animator>();
        }

        private void Update()
        {
            UpdateAnimatorParameters();
        }

        private void UpdateAnimatorParameters()
        {
            float speed = agent.velocity.magnitude;
            animator.SetFloat("speed", speed);
        }

        public void MoveTo(Vector3 position)
        {
            agent.SetDestination(position);
        }

        public void StopAgent()
        {
            agent.isStopped = true;
        }
    }
}
