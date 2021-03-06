﻿using System.Collections;
using System.Collections.Generic;
using TDS_MG.Attributes;
using TDS_MG.Saving;
using UnityEngine;

namespace TDS_MG.Combat
{
    public class WeaponCollection : MonoBehaviour, ISaveable
    {
        [SerializeField] CollectedWeapon[] collectedWeapons = new CollectedWeapon[0];

        List<Weapon> collection = new List<Weapon>();

        int weaponIndex = 0;

        private void Start()
        {
            InstantiateAllWeapons();
            SetUpInstantiatedWeaponStats();
            ResetWeaponTransformInCollecrion();
            HideAllWeapons();
        }

        private void InstantiateAllWeapons()
        {
            foreach (CollectedWeapon collectedWeapon in collectedWeapons)
            {
                if (collectedWeapon.isOwned && collectedWeapon.weapon != null)
                {
                    Weapon instantiatedWeapon = Instantiate(collectedWeapon.weapon, transform);
                    collection.Add(instantiatedWeapon);
                }
            }
        }

        private void SetUpInstantiatedWeaponStats()
        {
            foreach(Weapon weapon in collection)
            {
                WeaponStats stats = GetWeaponStats(weapon.GetWeaponType());
                weapon.SetDamage(stats.damage);
                weapon.SetTimeBetweenAttack(stats.fireRate);
                weapon.SetReloadSpeed(stats.reloadSpeed);
            }
        }

        private void ResetWeaponTransformInCollecrion()
        {
            collection.ForEach(weapon =>
            {
                weapon.transform.position = Vector3.zero;
                weapon.transform.rotation = Quaternion.Euler(Vector3.zero);
                weapon.transform.localScale = Vector3.one;
            });
        }

        public void HideAllWeapons() => collection.ForEach(weapon => weapon.gameObject.SetActive(false));

        public WeaponStats GetWeaponStats(WeaponType weaponType)
        {
            return GetCollectedWeapon(weaponType).weaponStats;
        }

        public void SetWeaponStats(WeaponType weaponType, WeaponStats stats)
        {
            GetCollectedWeapon(weaponType).weaponStats = stats;
        }

        public bool IsOwnedWeapon(WeaponType weaponType)
        {
            return GetCollectedWeapon(weaponType).isOwned;
        }

        public void TakePossession(WeaponType weaponType)
        {
            CollectedWeapon collectedWeapon = GetCollectedWeapon(weaponType);
            collectedWeapon.isOwned = true;
        }

        private CollectedWeapon GetCollectedWeapon(WeaponType weaponType)
        {
            foreach (CollectedWeapon collectedWeapon in collectedWeapons)
            {
                if (collectedWeapon.weapon == null)
                {
                    continue;
                }

                if (collectedWeapon.weapon.GetWeaponType() == weaponType)
                {
                    return collectedWeapon;
                }
            }

            return new CollectedWeapon();
        }

        public Sprite GetWeaponIcon(WeaponType weaponType)
        {
            foreach (CollectedWeapon collectedWeapon in collectedWeapons)
            {
                if (collectedWeapon.weapon == null || collectedWeapon.icon == null)
                {
                    continue;
                }

                if (collectedWeapon.weapon.GetWeaponType() == weaponType)
                {
                    return collectedWeapon.icon;
                }
            }

            return null;
        }

        public Weapon ShowDefaultWeapon()
        {
            Weapon weapon = GetWeapon(WeaponType.Pistol);
            ShowWeapon(weapon);

            return weapon;
        }

        private Weapon GetWeapon(WeaponType weaponType)
        {
            foreach (Weapon weapon in collection)
            {
                if (weapon.GetWeaponType() == weaponType)
                {
                    return weapon;
                }
            }

            return new Weapon();
        }

        public void ShowWeapon(Weapon weapon)
        {
            weapon.gameObject.SetActive(true);
        }

        public Weapon ShowNextWeapon()
        {
            Weapon weapon = collection[NextWeaponIndex()];
            ShowWeapon(weapon);

            return weapon;
        }

        private int NextWeaponIndex()
        {
            weaponIndex++;

            if (weaponIndex >= collection.Count)
            {
                weaponIndex = 0;
            }

            return weaponIndex;
        }

        public Weapon ShowPreviousWeapon()
        {
            Weapon weapon = collection[PreviousWeaponIndex()];
            ShowWeapon(weapon);

            return weapon;
        }

        private int PreviousWeaponIndex()
        {
            weaponIndex--;

            if (weaponIndex < 0)
            {
                weaponIndex = collection.Count - 1;
            }

            return weaponIndex;
        }

        public object CaptureState()
        {
            List<CollectedWeaponSaveData> saveDatas = new List<CollectedWeaponSaveData>();

            foreach (CollectedWeapon collectedWeapon in collectedWeapons)
            {
                CollectedWeaponSaveData weapon = new CollectedWeaponSaveData();
                weapon.isOwned = collectedWeapon.isOwned;
                weapon.weaponStats = collectedWeapon.weaponStats;

                saveDatas.Add(weapon);
            }

            return saveDatas;
        }

        public void RestoreState(object state)
        {
            List<CollectedWeaponSaveData> saveDatas = (List<CollectedWeaponSaveData>)state;

            for (int i = 0; i < collectedWeapons.Length; i++)
            {
                collectedWeapons[i].isOwned = saveDatas[i].isOwned;
                collectedWeapons[i].weaponStats = saveDatas[i].weaponStats;
            }
        }

        [System.Serializable]
        private class CollectedWeapon
        {
            public bool isOwned = false;
            public Weapon weapon = null;
            public Sprite icon = null;
            public WeaponStats weaponStats = new WeaponStats();
        }

        [System.Serializable]
        private class CollectedWeaponSaveData
        {
            public bool isOwned = false;
            public WeaponStats weaponStats = new WeaponStats();
        }
    }
}
