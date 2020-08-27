using System.Collections;
using System.Collections.Generic;
using TDS_MG.Combat;
using UnityEngine;
using UnityEngine.UI;

namespace TDS_MG.Shop
{
    public class GunShop : MonoBehaviour
    {
        [SerializeField] GameObject upgradeWeaponPanel = null;
        [SerializeField] Button buyButton = null;
        [SerializeField] float selectedIconScale = 1.1f;
        [Space]
        [SerializeField] GunShopItem[] gunShopItems = new GunShopItem[0];

        WeaponCollection weaponCollection;
        Image selectedIcon;

        private void Awake()
        {
            weaponCollection = GameObject.FindWithTag("Player").GetComponent<WeaponCollection>();
        }

        private void Start()
        {
            RefreshWeaponIcon();
            HideSubItemPanels();
        }

        public void SelectWeapon(Image icon, WeaponType weaponType)
        {
            HideSubItemPanels();

            if (selectedIcon != null)
            {
                RescaleImage(selectedIcon, 1f);
            }

            selectedIcon = icon;

            RescaleImage(icon, selectedIconScale);

            GunShopItem item = GetGunShopItem(weaponType);

            if (item != null && item.isOwned)
            {
                upgradeWeaponPanel.SetActive(true);
            }
            else
            {
                buyButton.gameObject.SetActive(true);
            }
        }

        private void RefreshWeaponIcon()
        {
            foreach (GunShopItem gunShopItem in gunShopItems)
            {
                gunShopItem.isOwned = weaponCollection.IsOwnedWeapon(gunShopItem.weaponType);

                if (gunShopItem.isOwned)
                {
                    gunShopItem.button.image.color = Color.white;
                }
                else
                {
                    Color color = Color.white;
                    color.a = 0.6f;

                    gunShopItem.button.image.color = color;
                }
            }
        }

        private GunShopItem GetGunShopItem(WeaponType weaponType)
        {
            foreach (GunShopItem gunShopItem in gunShopItems)
            {
                if (gunShopItem.weaponType == weaponType)
                {
                    return gunShopItem;
                }
            }

            return null;
        }

        private void RescaleImage(Image icon, float scale)
        {
            icon.gameObject.GetComponent<RectTransform>().localScale = Vector3.one * scale;
        }

        private void HideSubItemPanels()
        {
            upgradeWeaponPanel.SetActive(false);
            buyButton.gameObject.SetActive(false);
        }

        [System.Serializable]
        private class GunShopItem
        {
            public bool isOwned = false;
            public WeaponType weaponType = WeaponType.NoWeapon;
            public Button button = null;
            public int price = 0;
        }
    }
}
