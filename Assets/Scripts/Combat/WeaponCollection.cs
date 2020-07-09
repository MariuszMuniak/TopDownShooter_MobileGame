using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace TDS_MG.Combat
{
    public class WeaponCollection : MonoBehaviour
    {
        [SerializeField] Weapon[] weapons;

        List<Weapon> collection = new List<Weapon>();

        void Start()
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

        public Weapon ShowWeapon(WeaponType weaponType)
        {
            Weapon weapon = GetWeapon(weaponType);

            weapon.gameObject.SetActive(true);

            return weapon;
        }

        private void InstantiateAllWeapons()
        {
            foreach (Weapon weapon in weapons)
            {
                Weapon instantiatedWeapon = Instantiate(weapon, transform);

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
    }
}
