using System.Collections;
using System.Collections.Generic;
using TDS_MG.Shop;
using UnityEngine;

namespace TDS_MG.Attributes
{
    [System.Serializable]
    public class WeaponStatsProgressionPerUpgrationLevel
    {
        public WeaponType weaponType;
        public WeaponStatsWithPricePerUpgrade[] weaponStatsWithCostPerUpgrades = new WeaponStatsWithPricePerUpgrade[6];

        public int GetPrice(WeaponStats weaponStats, WeaponStatsUpgradeTypeEnum upgradeType)
        {
            for (int i = 0; i < weaponStatsWithCostPerUpgrades.Length - 1; i++)
            {
                switch (upgradeType)
                {
                    case WeaponStatsUpgradeTypeEnum.Damage:
                        if (weaponStatsWithCostPerUpgrades[i].weaponStats.damage == weaponStats.damage)
                        {
                            return weaponStatsWithCostPerUpgrades[i + 1].price;
                        }
                        break;
                    case WeaponStatsUpgradeTypeEnum.FireRate:
                        if (weaponStatsWithCostPerUpgrades[i].weaponStats.fireRate == weaponStats.fireRate)
                        {
                            return weaponStatsWithCostPerUpgrades[i + 1].price;
                        }
                        break;
                    case WeaponStatsUpgradeTypeEnum.ReloadSpeed:
                        if (weaponStatsWithCostPerUpgrades[i].weaponStats.reloadSpeed == weaponStats.reloadSpeed)
                        {
                            return weaponStatsWithCostPerUpgrades[i + 1].price;
                        }
                        break;
                    default:
                        break;
                }
            }

            return 0;
        }

        public int GetUpgradeLevel(WeaponStatsUpgradeTypeEnum statsType, WeaponStats weaponStats)
        {
            for (int i = 0; i < weaponStatsWithCostPerUpgrades.Length; i++)
            {
                switch (statsType)
                {
                    case WeaponStatsUpgradeTypeEnum.Damage:
                        if (weaponStats.damage == weaponStatsWithCostPerUpgrades[i].weaponStats.damage)
                        {
                            return i;
                        }
                        break;
                    case WeaponStatsUpgradeTypeEnum.FireRate:
                        if (weaponStats.fireRate == weaponStatsWithCostPerUpgrades[i].weaponStats.fireRate)
                        {
                            return i;
                        }
                        break;
                    case WeaponStatsUpgradeTypeEnum.ReloadSpeed:
                        if (weaponStats.reloadSpeed == weaponStatsWithCostPerUpgrades[i].weaponStats.reloadSpeed)
                        {
                            return i;
                        }
                        break;
                    default:
                        break;
                }
            }

            return 0;
        }

        public string GetUpgradeValue(WeaponStatsUpgradeTypeEnum statsType, int upgradeLevel)
        {
            for (int i = 0; i < weaponStatsWithCostPerUpgrades.Length; i++)
            {
                if (i != upgradeLevel)
                {
                    continue;
                }

                switch (statsType)
                {
                    case WeaponStatsUpgradeTypeEnum.Damage:
                        return weaponStatsWithCostPerUpgrades[i].weaponStats.damage.ToString();
                    case WeaponStatsUpgradeTypeEnum.FireRate:
                        return weaponStatsWithCostPerUpgrades[i].weaponStats.fireRate.ToString();
                    case WeaponStatsUpgradeTypeEnum.ReloadSpeed:
                        return weaponStatsWithCostPerUpgrades[i].weaponStats.reloadSpeed.ToString();
                    default:
                        return "0";
                }
            }

            return "0";
        }

        public WeaponStats GetDefaultWeaponStatsNotAsReference()
        {
            WeaponStats defaultStats = weaponStatsWithCostPerUpgrades[0].weaponStats;
            WeaponStats stats = new WeaponStats();
            stats.damage = defaultStats.damage;
            stats.fireRate = defaultStats.fireRate;
            stats.reloadSpeed = defaultStats.reloadSpeed;

            return stats;
        }
    }

    [System.Serializable]
    public class WeaponStatsWithPricePerUpgrade
    {
        public int price = 0;
        public WeaponStats weaponStats = new WeaponStats();
    }
}
