using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace TDS_MG.Combat
{
    public class Weapon : MonoBehaviour
    {
        [SerializeField] WeaponType weaponType = WeaponType.NoWeapon;
        [SerializeField] int damage = 10;
        [SerializeField] float timeBetweenAttack = 0.5f;
        [SerializeField] float range = 10f;
        [SerializeField] float radius = 0.5f;
        [SerializeField] Transform handle = null;
        [SerializeField] GameObject projectile = null;
        [SerializeField] GameObject muzzle = null;
        [SerializeField] WeaponComponents weaponComponents = new WeaponComponents();
        [Space]
        [SerializeField] Mesh gizmoMesh;

        float timeSinceLastAttack = Mathf.Infinity;

        private void Update()
        {
            timeSinceLastAttack += Time.deltaTime;
        }

        public float GetRange() => range;

        public Transform GetHandle() => handle;

        public WeaponType GetWeaponType() => weaponType;

        public bool CanAttack()
        {
            return timeSinceLastAttack >= timeBetweenAttack;
        }

        public void Fire()
        {
            Instantiate(muzzle, weaponComponents.Barrel.position, weaponComponents.Barrel.rotation);

            Vector3 localPoint = Vector3.zero;

            do
            {
                localPoint.x = Random.Range(-radius, radius);
                localPoint.y = Random.Range(-radius, radius);
            } while (localPoint.magnitude > radius);

            localPoint.z = range;

            GameObject instantiatedProjectil = Instantiate(projectile, weaponComponents.Barrel.position, weaponComponents.Barrel.rotation);
            Vector3 point = instantiatedProjectil.transform.TransformPoint(localPoint);
            instantiatedProjectil.transform.LookAt(point);

            timeSinceLastAttack = 0f;
        }

        private void OnDrawGizmos()
        {
            Gizmos.color = Color.black;
            Gizmos.DrawWireMesh(gizmoMesh, weaponComponents.Barrel.position, weaponComponents.Barrel.rotation, new Vector3(2 * radius, 2 * radius, range));
        }
    }
}
