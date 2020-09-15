using System.Collections;
using System.Collections.Generic;
using TDS_MG.Shop;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

namespace TDS_MG.UI
{
    [System.Serializable]
    public class GunShopItemUpgradeRowMenager
    {
        [SerializeField] WeaponStatsUpgradeTypeEnum upgradeType = WeaponStatsUpgradeTypeEnum.Damage;
        [SerializeField] TextMeshProUGUI upgradeNameText = null;
        [SerializeField] GridLayoutGroup weaponProgressionGridLayoutGroup = null;
        [SerializeField] GameObject upgradePriceBackground = null;
        [SerializeField] TextMeshProUGUI upgradePriceText = null;
        [SerializeField] Button upgradeButton = null;

        public WeaponStatsUpgradeTypeEnum GetUpgradeTyp()
        {
            return upgradeType;
        }

        public void SetPriceText(string price)
        {
            upgradePriceText.text = price;
        }

        public GridLayoutGroup GetGridLayoutGroup()
        {
            return weaponProgressionGridLayoutGroup;
        }

        public void ShowUpgradePriceText()
        {
            upgradePriceBackground.SetActive(true);
        }

        public void HideUpgradePriceText()
        {
            upgradePriceBackground.SetActive(false);
        }

        public void ShowUpgradeButton()
        {
            upgradeButton.gameObject.SetActive(true);
        }

        public void HideUpgradeButton()
        {
            upgradeButton.gameObject.SetActive(false);
        }

        public void ActivateWeaponUpgradeProgressionIcons(int upgradeLevel)
        {
            Image[] images = weaponProgressionGridLayoutGroup.GetComponentsInChildren<Image>();

            for (int i = 1; i < images.Length; i++)
            {
                images[i].enabled = i <= upgradeLevel;
            }
        }

        public bool IsMaxUpgradeProgressionLevel(int upgradeLevel)
        {
            Image[] images = weaponProgressionGridLayoutGroup.GetComponentsInChildren<Image>();
            return images.Length - 1 == upgradeLevel;
        }
    }
}