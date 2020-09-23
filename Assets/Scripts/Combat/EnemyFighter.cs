using System.Collections;
using System.Collections.Generic;
using TDS_MG.Attributes;
using UnityEngine;
using UnityEngine.AI;

namespace TDS_MG.Combat
{
    public class EnemyFighter : MonoBehaviour
    {
        [SerializeField] int damage = 15;
        [SerializeField] float timeBetweenAttacks = 1f;
        [SerializeField] BoxCollider rightHandCollider = null;
        [SerializeField] float minAttackSpeed = 0.4f;
        [SerializeField] float maxAttackSpeed = 0.8f;
        [SerializeField] GameObject hitEffect = null;

        Animator animator;
        Transform player;
        float attackRange;
        float timeSinceLastAttack = Mathf.Infinity;
        float attackSpeed;

        private void Awake()
        {
            animator = GetComponentInChildren<Animator>();
            player = GameObject.FindWithTag("Player").transform;
            attackRange = GetComponent<NavMeshAgent>().stoppingDistance;
        }

        private void Start()
        {
            attackSpeed = Random.Range(minAttackSpeed, maxAttackSpeed);
            animator.SetFloat("attackSpeed", attackSpeed);
            DeactivateRightHandCollider();
            SetUpMeleeAttack();
        }

        private void Update()
        {
            timeSinceLastAttack += Time.deltaTime;
        }

        public void DeactivateRightHandCollider()
        {
            rightHandCollider.enabled = false;
        }

        private void SetUpMeleeAttack()
        {
            rightHandCollider.isTrigger = true;
            EnemyMeleeAttack meleeAttack = rightHandCollider.gameObject.AddComponent<EnemyMeleeAttack>();
            meleeAttack.SetDamage(damage);
        }

        public void Attack()
        {
            if (!CanAttack()) { return; }

            animator.SetTrigger("meleeAttack");
            timeSinceLastAttack = 0f;
        }

        private bool CanAttack()
        {
            return timeSinceLastAttack > timeBetweenAttacks && DistanceToPlayer() <= attackRange;
        }

        private float DistanceToPlayer()
        {
            return Vector3.Distance(transform.position, player.position);
        }

        public void ActivateRightHandCollider()
        {
            rightHandCollider.enabled = true;
        }

        public void InstantiateHitEffect(Vector3 hitPositin)
        {
            Quaternion rotation = ReverseYRotation(transform.rotation);
            Instantiate(hitEffect, hitPositin, rotation);
        }

        private Quaternion ReverseYRotation(Quaternion rotation)
        {
            Vector3 reversedRotation = new Vector3
            {
                x = rotation.eulerAngles.x,
                y = rotation.eulerAngles.y - 180,
                z = rotation.eulerAngles.z
            };

            return Quaternion.Euler(reversedRotation);
        }
    }
}
