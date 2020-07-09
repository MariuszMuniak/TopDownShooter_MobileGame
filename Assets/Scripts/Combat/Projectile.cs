using System.Collections;
using System.Collections.Generic;
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
            ContactPoint contactPoint = collision.GetContact(0);

            if (hitEffect != null)
            {
                Instantiate(hitEffect, contactPoint.point, Quaternion.identity);
            }

            Destroy(gameObject);
        }

        public void SetUp(int damage, float speed, float lifetime)
        {
            this.damage = damage;
            this.speed = speed;
            Destroy(gameObject, lifetime);
        }
    }
}
