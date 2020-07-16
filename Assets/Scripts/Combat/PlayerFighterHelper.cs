using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace TDS_MG.Combat
{
    public class PlayerFighterHelper : MonoBehaviour
    {
        PlayerFighter fighter;

        private void Awake()
        {
            fighter = GetComponentInParent<PlayerFighter>();
        }

        public void RestoreAmmo()
        {
            fighter.RestoreAmmo();
        }

        public void FinishReloading()
        {
            fighter.FinishReloading();
        }
    }
}
