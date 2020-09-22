using System.Collections;
using System.Collections.Generic;
using TDS_MG.Attributes;
using TDS_MG.Combat;
using TDS_MG.SceneManagement;
using TDS_MG.UI;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

namespace TDS_MG.Shop
{
    public class GunShop : MonoBehaviour
    {
        [SerializeField] GunShopPanelsMenager panelsMenager = null;
        [SerializeField] GunShopItemUpgradePanel gunShopItemUpgradePanel = null;
        [SerializeField] GunShopItemBuyPanel gunShopItemBuyPanel = null;
        [SerializeField] float selectedIconScale = 1.1f;
        [SerializeField] WeaponStatsProgression weaponStatsProgression = null;
        [SerializeField] Color ownedGunShopItemButtonImageColor = Color.white;
        [SerializeField] Color notOwnedGunShopItemButtonImageColor = Color.white;
        [Space]
        [SerializeField] GunShopItem[] gunShopItems = new GunShopItem[0];

        WeaponCollection weaponCollection;
        Wallet wallet;
        GunShopItem selectedItem;
        Image selectedIcon;
        SavingWrapper savingWrapper;

        private void Awake()
        {
            weaponCollection = GameObject.FindWithTag("Player").GetComponent<WeaponCollection>();
            wallet = FindObjectOfType<Wallet>();
            savingWrapper = FindObjectOfType<SavingWrapper>();
        }

        private void Start()
        {
            panelsMenager.HidePanels();
            RefreshGunShopItemsConfiguration();
        }

        private void RefreshGunShopItemsConfiguration()
        {
            foreach (GunShopItem gunShopItem in gunShopItems)
            {
                gunShopItem.isOwned = weaponCollection.IsOwnedWeapon(gunShopItem.GetWeaponType());
                RefreshGunShopItemsIcon(gunShopItem);
            }
        }

        private void RefreshGunShopItemsIcon(GunShopItem item)
        {
            item.GetButtonChildrenImage().color = item.IsOwned() ? ownedGunShopItemButtonImageColor : notOwnedGunShopItemButtonImageColor;
        }

        public void SelectGunShopItem(WeaponType weaponType)
        {
            selectedItem = GetGunShopItem(weaponType);

            RescaleGunShopItemImage(selectedItem);
            ShowConfiguredGunShopItemPanel(selectedItem);
        }

        private GunShopItem GetGunShopItem(WeaponType weaponType)
        {
            foreach (GunShopItem gunShopItem in gunShopItems)
            {
                if (gunShopItem.GetWeaponType() == weaponType)
                {
                    return gunShopItem;
                }
            }

            return new GunShopItem();
        }

        private void RescaleGunShopItemImage(GunShopItem selectedItem)
        {
            if (selectedIcon != null)
            {
                ScaleSelectedIcon(1f);
            }

            selectedIcon = selectedItem.GetButtonImage();
            ScaleSelectedIcon(selectedIconScale);
        }

        private void ScaleSelectedIcon(float scale)
        {
            selectedIcon.gameObject.GetComponent<RectTransform>().localScale = Vector3.one * scale;
        }

        private void ShowConfiguredGunShopItemPanel(GunShopItem item)
        {
            gunShopItemBuyPanel.SetSelectedGunShopItemIcon(weaponCollection.GetWeaponIcon(item.GetWeaponType()));

            if (item.IsOwned())
            {
                ConfigureWeaponUpgradePanel(item);
                panelsMenager.OnlyShowWeaponUpgradePanel();
            }
            else
            {
                ConfigureBuyPanel(item);
                panelsMenager.OnlyShowBuyWeaponPanel();
            }
        }

        private void ConfigureWeaponUpgradePanel(GunShopItem item)
        {
            GunShopItemUpgradeRow[] rows = SetUpGunShopItemUpgradeRows(item);
            gunShopItemUpgradePanel.RefreshGunShopItemUpgradeRows(rows);
        }

        private GunShopItemUpgradeRow[] SetUpGunShopItemUpgradeRows(GunShopItem item)
        {
            WeaponStats weaponStats = weaponCollection.GetWeaponStats(item.GetWeaponType());
            WeaponStatsUpgradeLevel weaponStatsUpgradeLevel = new WeaponStatsUpgradeLevel(item.GetWeaponType(), weaponStats, weaponStatsProgression);

            GunShopItemUpgradeRow damageRow = new GunShopItemUpgradeRow(WeaponStatsUpgradeTypeEnum.Damage);
            damageRow.price = weaponStatsProgression.GetPrice(item.GetWeaponType(), weaponStats, WeaponStatsUpgradeTypeEnum.Damage).ToString();
            damageRow.upgradeLevel = weaponStatsUpgradeLevel.GetUpgradeLevel(damageRow.upgradeType);

            GunShopItemUpgradeRow fireRateRow = new GunShopItemUpgradeRow(WeaponStatsUpgradeTypeEnum.FireRate);
            fireRateRow.price = weaponStatsProgression.GetPrice(item.GetWeaponType(), weaponStats, fireRateRow.upgradeType).ToString();
            fireRateRow.upgradeLevel = weaponStatsUpgradeLevel.GetUpgradeLevel(fireRateRow.upgradeType);

            GunShopItemUpgradeRow reloadSpeedRow = new GunShopItemUpgradeRow(WeaponStatsUpgradeTypeEnum.ReloadSpeed);
            reloadSpeedRow.price = weaponStatsProgression.GetPrice(item.GetWeaponType(), weaponStats, reloadSpeedRow.upgradeType).ToString();
            reloadSpeedRow.upgradeLevel = weaponStatsUpgradeLevel.GetUpgradeLevel(reloadSpeedRow.upgradeType);

            return new GunShopItemUpgradeRow[] { damageRow, fireRateRow, reloadSpeedRow };
        }

        private void ConfigureBuyPanel(GunShopItem item)
        {
            gunShopItemBuyPanel.SetPrice(item.GetPrice());
        }

        public void UpgradeSelectedGunShopItem(WeaponStatsUpgradeTypeEnum upgradeType)
        {
            WeaponStats stats = UpgradeSelectedItemWeaponStats(upgradeType);
            int price = weaponStatsProgression.GetPrice(selectedItem.GetWeaponType(), stats, upgradeType);

            if (CanBuy(price))
            {
                wallet.SpendMoney(price);
                weaponCollection.SetWeaponStats(selectedItem.GetWeaponType(), stats);
                ShowConfiguredGunShopItemPanel(selectedItem);
                savingWrapper.Save();
            }
        }

        private WeaponStats UpgradeSelectedItemWeaponStats(WeaponStatsUpgradeTypeEnum upgradeType)
        {
            WeaponStats stats = weaponCollection.GetWeaponStats(selectedItem.GetWeaponType());
            WeaponStatsUpgradeLevel weaponStatsUpgradeLevel = new WeaponStatsUpgradeLevel(selectedItem.GetWeaponType(), stats, weaponStatsProgression);
            string upgradeValu = weaponStatsUpgradeLevel.GetUpgradeValue(upgradeType, weaponStatsUpgradeLevel.GetUpgradeLevel(upgradeType) + 1);

            switch (upgradeType)
            {
                case WeaponStatsUpgradeTypeEnum.Damage:
                    stats.damage = int.Parse(upgradeValu);
                    break;
                case WeaponStatsUpgradeTypeEnum.FireRate:
                    stats.fireRate = float.Parse(upgradeValu);
                    break;
                case WeaponStatsUpgradeTypeEnum.ReloadSpeed:
                    stats.reloadSpeed = float.Parse(upgradeValu);
                    break;
            }

            return stats;
        }

        public void BuySelectedGunShopItem()
        {
            if (CanBuy(selectedItem.GetPrice()))
            {
                wallet.SpendMoney(selectedItem.GetPrice());
                selectedItem.isOwned = true;
                weaponCollection.TakePossession(selectedItem.GetWeaponType());
                SetDefaultWeaponStats(selectedItem.GetWeaponType());
                RefreshGunShopItemsConfiguration();
                ShowConfiguredGunShopItemPanel(selectedItem);
                savingWrapper.Save();
            }
        }

        private bool CanBuy(int price)
        {
            return wallet.HaveEnoughMoney(price);
        }

        private void SetDefaultWeaponStats(WeaponType weaponType)
        {
            WeaponStats stats = weaponStatsProgression.GetDefaultWeaponStatsNotAsReference(weaponType);
            weaponCollection.SetWeaponStats(weaponType, stats);
        }

        private class WeaponStatsUpgradeLevel
        {
            WeaponType weaponType;
            WeaponStats currentWeaponStats;
            WeaponStatsProgression weaponStatsProgression;

            public WeaponStatsUpgradeLevel(WeaponType weaponType, WeaponStats currentWeaponStats, WeaponStatsProgression weaponStatsProgression)
            {
                this.weaponType = weaponType;
                this.currentWeaponStats = currentWeaponStats;
                this.weaponStatsProgression = weaponStatsProgression;
            }

            public int GetUpgradeLevel(WeaponStatsUpgradeTypeEnum statsType)
            {
                return weaponStatsProgression.GetUpgradeLevel(weaponType, statsType, currentWeaponStats);
            }

            public string GetUpgradeValue(WeaponStatsUpgradeTypeEnum upgradeType, int upgradeLevel)
            {
                return weaponStatsProgression.GetUpgradeValue(weaponType, upgradeType, upgradeLevel);
            }
        }
    }
}
