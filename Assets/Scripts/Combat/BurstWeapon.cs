using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace TDS_MG.Combat
{
    public class BurstWeapon : Weapon
    {
        [SerializeField] int projectilseInBurst = 10;

        protected override void InstantiateAndSetUpProjectile()
        {
            for (int i = 0; i < projectilseInBurst; i++)
            {
                GameObject instantiatedProjectil = InstantiatedProjectile();
                SetUpProjectile(instantiatedProjectil);
            }
        }

        protected override void SetUpProjectile(GameObject instantiatedProjectil)
        {
            Vector3 point = instantiatedProjectil.transform.TransformPoint(GetPointFromCircleWithMaxRange());
            instantiatedProjectil.transform.LookAt(point);

            instantiatedProjectil.GetComponent<Projectile>().SetUp(damage / projectilseInBurst, projectileSpeed, GetMaxLifetime());
        }
    } 
}
