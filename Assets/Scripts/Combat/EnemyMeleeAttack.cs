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

        private void Awake()
        {
            boxCollider = GetComponent<BoxCollider>();
        }

        private void OnTriggerEnter(Collider other)
        {
            if (other.gameObject.CompareTag("Player"))
            {
                Health health = other.gameObject.GetComponent<Health>();

                if (health != null)
                {
                    health.TakeDamage(damage); 
                }

                boxCollider.enabled = false;
            }
        }

        public void SetDamage(int damage)
        {
            this.damage = damage;
        }
    }
}
