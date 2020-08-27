using System.Collections;
using System.Collections.Generic;
using TDS_MG.Shop;
using UnityEngine;
using UnityEngine.UI;

namespace TDS_MG.UI
{
    public class SelectShopItemOnClick : MonoBehaviour
    {
        [SerializeField] WeaponType weaponType = WeaponType.NoWeapon;

        Button button;
        GunShop gunShop;
        Image icon;

        private void Awake()
        {
            button = GetComponent<Button>();
            gunShop = FindObjectOfType<GunShop>();
            icon = GetComponent<Image>();
        }

        private void Start()
        {
            if (gunShop != null)
            {
                button.onClick.AddListener(() => gunShop.SelectWeapon(icon, weaponType));
            }
        }
    }
}
