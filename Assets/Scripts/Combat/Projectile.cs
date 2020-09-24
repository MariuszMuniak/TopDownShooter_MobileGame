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
            InstantiateHitEffect(collision.GetContact(0).point);
            EnemyTakeDamage(collision.gameObject);

            if (collision.gameObject.GetComponent<Projectile>() == null)
            {
                Destroy(gameObject);
            }
        }

        private void InstantiateHitEffect(Vector3 atPoint)
        {
            if (hitEffect != null)
            {
                Instantiate(hitEffect, atPoint, Quaternion.identity);
            }
        }

        private void EnemyTakeDamage(GameObject hitenObject)
        {
            Health health = null;

            if (hitenObject.CompareTag("Enemy"))
            {
                health = hitenObject.gameObject.GetComponentInParent<Health>();
            }

            if (health != null)
            {
                health.TakeDamage(damage);
            }
        }

        public void SetUp(int damage, float speed, float lifetime)
        {
            this.damage = damage;
            this.speed = speed;
            Destroy(gameObject, lifetime);
        }
    }
}
