using System.Collections;
using System.Collections.Generic;
using TDS_MG.Attributes;
using UnityEngine;

namespace TDS_MG.Combat
{
    public class Projectile : MonoBehaviour
    {
        [SerializeField] GameObject hitEffect = null;

        int damage = 0;
        float speed = 0f;

        void Update()
        {
            transform.Translate(Vector3.forward * speed * Time.deltaTime);
        }

        private void OnCollisionEnter(Collision collision)
        {
            InstantiateHitEffect(collision);
            EnemyTakeDamage(collision);
            Destroy(gameObject);
        }

        public void SetUp(int damage, float speed, float lifetime)
        {
            this.damage = damage;
            this.speed = speed;
            Destroy(gameObject, lifetime);
        }

        private void InstantiateHitEffect(Collision collision)
        {
            ContactPoint contactPoint = collision.GetContact(0);

            if (hitEffect != null)
            {
                Instantiate(hitEffect, contactPoint.point, Quaternion.identity);
            }
        }

        private void EnemyTakeDamage(Collision collision)
        {
            Health health = null;

            if (collision.gameObject.CompareTag("Enemy"))
            {
                health = collision.gameObject.GetComponentInParent<Health>();
            }

            if (health != null)
            {
                health.TakeDamage(damage);
            }
        }
    }
}
