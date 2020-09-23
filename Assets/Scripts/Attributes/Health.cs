using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

namespace TDS_MG.Attributes
{
    public class Health : MonoBehaviour
    {
        [SerializeField] int maxHealth = 100;

        [HideInInspector] public UnityEvent OnDeath;

        int currentHealth;
        bool isDead = false;

        void Start()
        {
            currentHealth = maxHealth;
        }

        public float GetHealthPercentage()
        {
            return (float)currentHealth / maxHealth;
        }

        public void RestoreHealth(int amount)
        {
            currentHealth = Mathf.Min(currentHealth + amount, maxHealth);
        }

        public void TakeDamage(int damage)
        {
            currentHealth = Mathf.Max(currentHealth - damage, 0);

            if (currentHealth == 0 && !isDead)
            {
                Die();
            }
        }

        public bool IsDead()
        {
            return isDead;
        }

        private void Die()
        {
            isDead = true;
            UpdateAnimatorParameters(RandomDeathTypeIndex());
            OnDeath.Invoke();
        }

        private int RandomDeathTypeIndex()
        {
            return gameObject.CompareTag("Player") ? Random.Range(1, 3) : Random.Range(1, 5);
        }

        private void UpdateAnimatorParameters(int deathTypeIndex)
        {
            Animator animator = GetComponentInChildren<Animator>();
            animator.SetBool("Death_b", isDead);
            animator.SetInteger("DeathType_int", deathTypeIndex);
        }
    }
}
