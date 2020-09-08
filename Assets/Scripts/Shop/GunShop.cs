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
        [SerializeField] Color ownedGunShopItemButtonImageColor = Color.white;
        [SerializeField] Color notOwnedGunShopItemButtonImageColor = Color.white;
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
            RefreshGunShopItemsConfiguration();
            HideGunShopItemPanels();
        }

        private void RefreshGunShopItemsConfiguration()
        {
            foreach (GunShopItem gunShopItem in gunShopItems)
            {
                gunShopItem.isOwned = weaponCollection.IsOwnedWeapon(gunShopItem.weaponType);
                RefreshGunShopItemsIcon(gunShopItem);
            }
        }

        private void RefreshGunShopItemsIcon(GunShopItem item)
        {
            item.GetImageFromButton().color = item.isOwned ? ownedGunShopItemButtonImageColor : notOwnedGunShopItemButtonImageColor;
        }

        private void HideGunShopItemPanels()
        {
            upgradeWeaponPanel.SetActive(false);
            buyButton.gameObject.SetActive(false);
        }

        public void SelectWeapon(WeaponType weaponType)
        {
            GunShopItem selectedItem = GetGunShopItem(weaponType);

            RescaleGunShopItemImage(selectedItem);
            HideGunShopItemPanels();
            ShowGunShopItemPanel(selectedItem);
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

        private void RescaleGunShopItemImage(GunShopItem selectedItem)
        {
            if (selectedIcon != null)
            {
                ScaleSelectedIcon(1f);
            }

            selectedIcon = selectedItem.GetImageFromButton();
            ScaleSelectedIcon(selectedIconScale);
        }

        private void ScaleSelectedIcon(float scale)
        {
            selectedIcon.gameObject.GetComponent<RectTransform>().localScale = Vector3.one * scale;
        }

        private void ShowGunShopItemPanel(GunShopItem item)
        {
            if (item.isOwned)
            {
                upgradeWeaponPanel.SetActive(true);
            }
            else
            {
                buyButton.gameObject.SetActive(true);
            }
        }
    }
}
