using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace TDS_MG.Attributes
{
    public class Health : MonoBehaviour
    {
        [SerializeField] int maxHealth = 100;
        [SerializeField] int currentHealth = 0;

        public int MaxHealth { get { return maxHealth; } }
        public int CurrentHealth { get { return currentHealth; } }

        void Start()
        {
            currentHealth = maxHealth;
        }

        public void TakeDamage(int damage)
        {
            currentHealth = Mathf.Max(currentHealth - damage, 0);
        }

        public void RestoreHealth(int amount)
        {
            currentHealth = Mathf.Min(currentHealth + amount, maxHealth);
        }
    }
}
