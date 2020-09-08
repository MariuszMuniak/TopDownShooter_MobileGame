using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

namespace TDS_MG.Attributes
{
    public class Health : MonoBehaviour
    {
        [SerializeField] int maxHealth = 100;

        [HideInInspector] public int MaxHealth { get { return maxHealth; } }
        [HideInInspector] public int CurrentHealth { get; private set; }
        [HideInInspector] public UnityEvent OnDeath;

        bool isDead = false;

        void Start()
        {
            CurrentHealth = maxHealth;
        }

        public bool IsDead()
        {
            return isDead;
        }

        public void RestoreHealth(int amount)
        {
            CurrentHealth = Mathf.Min(CurrentHealth + amount, MaxHealth);
        }

        public void TakeDamage(int damage)
        {
            CurrentHealth = Mathf.Max(CurrentHealth - damage, 0);

            if (CurrentHealth == 0 && !isDead)
            {
                Die();
            }
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
