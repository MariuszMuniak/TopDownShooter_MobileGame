using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace TDS_MG.Combat
{
    public class Weapon : MonoBehaviour
    {
        [SerializeField] WeaponType weaponType = WeaponType.NoWeapon;
        [SerializeField] int damage = 10;
        [SerializeField] int magazineSize = 30;
        [SerializeField] float timeBetweenAttack = 0.5f;
        [SerializeField] float range = 10f;
        [SerializeField] float radius = 0.5f;
        [SerializeField] float projectileSpeed = 10f;
        [SerializeField] GameObject projectile = null;
        [SerializeField] GameObject muzzle = null;
        [SerializeField] WeaponComponents weaponComponents = new WeaponComponents();
        [Space]
        [SerializeField] Mesh gizmoMesh = null;

        int ammoInMagazine = 0;
        float timeSinceLastAttack = Mathf.Infinity;

        private void Start()
        {
            ammoInMagazine = magazineSize;
        }

        private void Update()
        {
            timeSinceLastAttack += Time.deltaTime;
        }

        public int GetAmmoInMagazine() => ammoInMagazine;

        public int GetMagazineSize() => magazineSize;

        public float GetRange() => range;

        public WeaponType GetWeaponType() => weaponType;

        public bool CanAttack()
        {
            bool hitEnemy = false;

            if (Physics.Raycast(weaponComponents.Barrel.position, weaponComponents.Barrel.forward, out RaycastHit hit, range))
            {
                hitEnemy = hit.collider.gameObject.CompareTag("Enemy");
            }

            return !MagazineIsEmpty() && timeSinceLastAttack >= timeBetweenAttack && hitEnemy;
        }

        public void Fire()
        {
            Instantiate(muzzle, weaponComponents.Barrel.position, weaponComponents.Barrel.rotation, weaponComponents.Barrel);

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
            instantiatedProjectil.GetComponent<Projectile>().SetUp(damage, projectileSpeed, GetMaxLifetime());

            ReduceAmmoInzMagazine();

            timeSinceLastAttack = 0f;
        }

        public bool MagazineIsEmpty()
        {
            return ammoInMagazine <= 0;
        }

        public void RestoreAmmo()
        {
            ammoInMagazine = magazineSize;
        }

        private float GetMaxLifetime()
        {
            float lifetime = range / projectileSpeed;

            return lifetime;
        }

        private void ReduceAmmoInzMagazine()
        {
            ammoInMagazine = Mathf.Max(ammoInMagazine - 1, 0);
        }

        private void OnDrawGizmos()
        {
            Gizmos.color = Color.black;
            Gizmos.DrawWireMesh(gizmoMesh, weaponComponents.Barrel.position, weaponComponents.Barrel.rotation, new Vector3(2 * radius, 2 * radius, range));
        }
    }
}
