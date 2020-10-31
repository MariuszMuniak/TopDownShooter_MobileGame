using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace TDS_MG.Combat
{
    public class Weapon : MonoBehaviour
    {
        [SerializeField] WeaponType weaponType = WeaponType.NoWeapon;
        [SerializeField] protected int damage = 10;
        [SerializeField] int magazineSize = 30;
        [SerializeField] float timeBetweenAttack = 0.5f;
        [SerializeField] float range = 10f;
        [SerializeField] float radius = 0.5f;
        [SerializeField] protected float projectileSpeed = 10f;
        [SerializeField] GameObject projectile = null;
        [SerializeField] GameObject muzzleEffect = null;
        [SerializeField] LineRenderer laser = null;
        [SerializeField] protected WeaponComponents weaponComponents = new WeaponComponents();
        [SerializeField] AudioSource audioSource = null;
        [SerializeField] AudioClip shotSound = null;
        [SerializeField] AudioClip reloadSound = null;
        [Space]
        [SerializeField] Mesh gizmoMesh = null;

        int ammoInMagazine = 0;
        float timeSinceLastAttack = Mathf.Infinity;
        float reloadSpeed = 3f;

        private void Awake()
        {
            ammoInMagazine = magazineSize;
        }

        private void Update()
        {
            timeSinceLastAttack += Time.deltaTime;
            Laser();
        }

        private void Laser()
        {
            if (laser != null && weaponComponents.Laser.activeSelf)
            {
                laser.SetPosition(0, laser.transform.position);

                if (Physics.Raycast(laser.transform.position, laser.transform.forward, out RaycastHit hit, range))
                {
                    laser.SetPosition(1, hit.point);
                }
                else
                {
                    laser.SetPosition(1, laser.transform.TransformPoint(Vector3.forward * range * 1.54f));
                }
            }
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

            return !MagazineIsEmpty() && timeSinceLastAttack >= timeBetweenAttack && (hitEnemy || IsEnemyInCloseRange());
        }

        public bool MagazineIsEmpty()
        {
            return ammoInMagazine <= 0;
        }

        bool IsEnemyInCloseRange()
        {
            RaycastHit[] hits = Physics.SphereCastAll(transform.position, 1f, transform.forward, 0f);

            foreach(RaycastHit hit in hits)
            {
                if (hit.collider.CompareTag("Enemy"))
                {
                    return true;
                }
            }

            return false;
        }

        public void Fire()
        {
            InstantiateAndSetUpProjectile();
            InstantiateMuzzleEffect();
            ReduceAmmoInzMagazine();
            PlayShotSound();

            timeSinceLastAttack = 0f;
        }

        protected virtual void InstantiateAndSetUpProjectile()
        {
            GameObject instantiatedProjectil = InstantiatedProjectile();
            SetUpProjectile(instantiatedProjectil);
        }

        protected GameObject InstantiatedProjectile()
        {
            return Instantiate(projectile, weaponComponents.Barrel.position, weaponComponents.Barrel.rotation);
        }

        protected virtual void SetUpProjectile(GameObject instantiatedProjectil)
        {
            Vector3 point = instantiatedProjectil.transform.TransformPoint(GetPointFromCircleWithMaxRange());
            instantiatedProjectil.transform.LookAt(point);

            instantiatedProjectil.GetComponent<Projectile>().SetUp(damage, projectileSpeed, GetMaxLifetime());
        }

        protected Vector3 GetPointFromCircleWithMaxRange()
        {
            Vector3 localPoint = Vector3.zero;

            do
            {
                localPoint.x = Random.Range(-radius, radius);
                localPoint.y = Random.Range(-radius, radius);
            } while (localPoint.magnitude > radius);

            localPoint.z = range;

            return localPoint;
        }

        protected float GetMaxLifetime()
        {
            float lifetime = range / projectileSpeed;
            return lifetime;
        }

        private void InstantiateMuzzleEffect()
        {
            if (muzzleEffect != null)
            {
                Instantiate(muzzleEffect, weaponComponents.Barrel.position, weaponComponents.Barrel.rotation, weaponComponents.Barrel);
            }
        }

        private void ReduceAmmoInzMagazine()
        {
            ammoInMagazine = Mathf.Max(ammoInMagazine - 1, 0);
        }

        private void PlayShotSound()
        {
            if (shotSound != null)
            {
                audioSource.PlayOneShot(shotSound);
            }
        }

        public void PlayReloadSound()
        {
            if (reloadSound != null)
            {
                audioSource.PlayOneShot(reloadSound);
            }
        }

        public void RestoreAmmo()
        {
            ammoInMagazine = magazineSize;
        }

        public float GetReloadSpeed()
        {
            return reloadSpeed;
        }

        public float GetTimeBetweenAttack()
        {
            return timeBetweenAttack;
        }

        public void SetDamage(int damage)
        {
            this.damage = damage;
        }

        public void SetTimeBetweenAttack(float fireRate)
        {
            timeBetweenAttack = 1f / fireRate;
        }

        public void SetReloadSpeed(float reloadSpeed)
        {
            this.reloadSpeed = reloadSpeed;
        }

        private void OnDrawGizmos()
        {
            Gizmos.color = Color.black;
            Gizmos.DrawWireMesh(gizmoMesh, weaponComponents.Barrel.position, weaponComponents.Barrel.rotation, new Vector3(2 * radius, 2 * radius, range));
        }
    }
}
