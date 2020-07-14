using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace TDS_MG.Combat
{
    public class EnemyFighterHelper : MonoBehaviour
    {
        EnemyFighter fighter;

        private void Awake()
        {
            fighter = GetComponentInParent<EnemyFighter>();
        }

        public void ActivateRightHandCollider()
        {
            fighter.ActivateRightHandCollider();
        }

        public void DeactivateRightHandCollider()
        {
            fighter.DeactivateRightHandCollider();
        }
    }
}
