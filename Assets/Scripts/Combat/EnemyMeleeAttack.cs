using System.Collections;
using System.Collections.Generic;
using TDS_MG.Attributes;
using UnityEngine;

namespace TDS_MG.Combat
{
    public class EnemyMeleeAttack : MonoBehaviour
    {
        int damage;
        BoxCollider boxCollider;
        EnemyFighter fighter;

        private void Awake()
        {
            boxCollider = GetComponent<BoxCollider>();
            fighter = GetComponentInParent<EnemyFighter>();
        }

        private void Start()
        {
            boxCollider.isTrigger = false;
        }

        private void OnCollisionEnter(Collision collision)
        {
            if (collision.gameObject.CompareTag("Player"))
            {
                Health health = collision.gameObject.GetComponent<Health>();

                if (health != null)
                {
                    health.TakeDamage(damage);
                }

                fighter.InstantiateHitEffect(collision.contacts[0].point);

                boxCollider.enabled = false;
            }
        }

        public void SetDamage(int damage)
        {
            this.damage = damage;
        }
    }
}
