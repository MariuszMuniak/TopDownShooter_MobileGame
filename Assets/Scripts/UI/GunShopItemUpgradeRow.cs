using System.Collections;
using System.Collections.Generic;
using TDS_MG.Shop;

namespace TDS_MG.UI
{
    public class GunShopItemUpgradeRow
    {
        public WeaponStatsUpgradeTypeEnum upgradeType { get; private set; }
        public string price;
        public int upgradeLevel;

        public GunShopItemUpgradeRow(WeaponStatsUpgradeTypeEnum upgradeType)
        {
            this.upgradeType = upgradeType;
        }
    }
}
