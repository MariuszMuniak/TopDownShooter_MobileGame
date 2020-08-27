using System.Collections;
using System.Collections.Generic;
using TDS_MG.UI;
using UnityEngine;

namespace TDS_MG.Combat
{
    public class WeaponCollection : MonoBehaviour
    {
        [SerializeField] CollectedWeapon[] collectedWeapons = new CollectedWeapon[0];

        List<Weapon> collection = new List<Weapon>();

        int weaponIndex = 0;

        private void Awake()
        {
            InstantiateAllWeapons();
            HideAllWeapons();
        }

        public void HideAllWeapons()
        {
            foreach (Weapon weapon in collection)
            {
                weapon.gameObject.SetActive(false);

                weapon.gameObject.transform.parent = transform;
                weapon.gameObject.transform.localPosition = Vector3.zero;
                weapon.gameObject.transform.localRotation = Quaternion.Euler(Vector3.zero);
                weapon.gameObject.transform.localScale = Vector3.one;
            }
        }

        public Sprite GetWeaponIcon(WeaponType weaponType)
        {
            foreach (CollectedWeapon collectedWeapon in collectedWeapons)
            {
                if (collectedWeapon.weapon == null || collectedWeapon.icon == null) { continue; }

                if (collectedWeapon.weapon.GetWeaponType() == weaponType)
                {
                    return collectedWeapon.icon;
                }
            }

            return null;
        }

        public Weapon NextWeapon()
        {
            NextWeaponIndex();

            Weapon weapon = collection[weaponIndex];

            return ShowWeapon(weapon);
        }

        public Weapon PreviousWeapon()
        {
            PreviousWeaponIndex();

            Weapon weapon = collection[weaponIndex];

            return ShowWeapon(weapon);
        }

        public Weapon ShowWeapon(Weapon weapon)
        {
            weapon.gameObject.SetActive(true);

            return weapon;
        }

        public bool IsOwnedWeapon(WeaponType weaponType)
        {
            foreach (CollectedWeapon collectedWeapon in collectedWeapons)
            {
                if (collectedWeapon.weapon == null) { return false; }

                if (collectedWeapon.weapon.GetWeaponType() == weaponType)
                {
                    return collectedWeapon.isOwned;
                }
            }

            return false;
        }

        public Weapon ShowDefaultWeapon()
        {
            Weapon weapon = GetWeapon(WeaponType.Pistol);

            return ShowWeapon(weapon);
        }

        private void InstantiateAllWeapons()
        {
            foreach (CollectedWeapon collectedWeapon in collectedWeapons)
            {
                if (collectedWeapon.weapon == null) { continue; }

                Weapon instantiatedWeapon = Instantiate(collectedWeapon.weapon, transform);

                instantiatedWeapon.gameObject.transform.position = Vector3.zero;
                instantiatedWeapon.gameObject.transform.rotation = Quaternion.Euler(Vector3.zero);
                instantiatedWeapon.gameObject.transform.localScale = Vector3.one;

                collection.Add(instantiatedWeapon);
            }
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

            return null;
        }

        private void NextWeaponIndex()
        {
            weaponIndex++;

            if (weaponIndex >= collection.Count)
            {
                weaponIndex = 0;
            }
        }

        private void PreviousWeaponIndex()
        {
            weaponIndex--;

            if (weaponIndex < 0)
            {
                weaponIndex = collection.Count - 1;
            }
        }

        [System.Serializable]
        private class CollectedWeapon
        {
            public bool isOwned = false;
            public Weapon weapon = null;
            public Sprite icon = null;
        }
    }
}
