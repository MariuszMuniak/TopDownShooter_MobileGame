using System.Collections;
using System.Collections.Generic;
using TDS_MG.Control;
using UnityEngine;

namespace TDS_MG.Attributes
{
    public class EnemyHealth : Health
    {
        [SerializeField] int[] maxHealthPerLevel = new int[0];

        void Awake()
        {
            int index = FindObjectOfType<LevelController>().GecCurrentSceneBuildIndex() - 1;
            maxHealth = maxHealthPerLevel[index];
        }
    } 
}
