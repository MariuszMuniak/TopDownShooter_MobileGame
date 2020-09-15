using System.Collections;
using System.Collections.Generic;
using TDS_MG.Shop;
using UnityEngine;
using UnityEngine.UI;

namespace TDS_MG.UI
{
    public class GunShopItemUpgradePanel : MonoBehaviour
    {
        [SerializeField] GunShopItemUpgradeRowMenager[] gunShopItemUpgradeRows = new GunShopItemUpgradeRowMenager[0];

        public void RefreshGunShopItemUpgradeRows(GunShopItemUpgradeRow[] rows)
        {
            foreach (GunShopItemUpgradeRow row in rows)
            {
                RefreshGunShopItemUpgradeRow(row);
            }
        }

        public void RefreshGunShopItemUpgradeRow(GunShopItemUpgradeRow row)
        {
            GunShopItemUpgradeRowMenager rowMenager = GetUpgradeRow(row.upgradeType);
            rowMenager.SetPriceText(row.price);
            rowMenager.ActivateWeaponUpgradeProgressionIcons(row.upgradeLevel);

            if (rowMenager.IsMaxUpgradeProgressionLevel(row.upgradeLevel))
            {
                rowMenager.HideUpgradeButton();
                rowMenager.HideUpgradePriceText();
            }
            else
            {
                rowMenager.ShowUpgradeButton();
                rowMenager.ShowUpgradePriceText();
            }
        }

        private GunShopItemUpgradeRowMenager GetUpgradeRow(WeaponStatsUpgradeTypeEnum upgradeType)
        {
            foreach (GunShopItemUpgradeRowMenager gunShopItemUpgradeRow in gunShopItemUpgradeRows)
            {
                if (gunShopItemUpgradeRow.GetUpgradeTyp() == upgradeType)
                {
                    return gunShopItemUpgradeRow;
                }
            }

            return new GunShopItemUpgradeRowMenager();
        }
    }
}
