using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

namespace TDS_MG.Attributes
{
    public class Health : MonoBehaviour
    {
        [SerializeField] int maxHealth = 100;
        [SerializeField] int currentHealth = 0;

        [HideInInspector] public UnityEvent OnDeath;

        public int MaxHealth { get { return maxHealth; } }
        public int CurrentHealth { get { return currentHealth; } }
        public bool IsDead { get { return isDead; } }

        bool isDead = false;

        void Start()
        {
            currentHealth = maxHealth;
        }

        public void TakeDamage(int damage)
        {
            currentHealth = Mathf.Max(currentHealth - damage, 0);

            if (currentHealth == 0 && !isDead)
            {
                Die();
            }
        }

        public void RestoreHealth(int amount)
        {
            currentHealth = Mathf.Min(currentHealth + amount, maxHealth);
        }

        private void Die()
        {
            isDead = true;
            int deathType = Random.Range(1, 3);

            Animator animator = GetComponentInChildren<Animator>();
            animator.SetBool("Death_b", isDead);
            animator.SetInteger("DeathType_int", deathType);

            OnDeath.Invoke();
        }
    }
}
