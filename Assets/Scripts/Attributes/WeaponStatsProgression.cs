using System.Collections;
using System.Collections.Generic;
using TDS_MG.Shop;
using UnityEngine;

namespace TDS_MG.Attributes
{
    [CreateAssetMenu(fileName = "WeaponStatsProgression", menuName = "Attributes/New Weapon Stats Progression", order = 0)]
    public class WeaponStatsProgression : ScriptableObject
    {
        [SerializeField] WeaponStatsProgressionPerUpgrationLevel[] weaponStatsProgressionPerUpgrationLevels = new WeaponStatsProgressionPerUpgrationLevel[0];

        public int GetPrice(WeaponType weaponType, WeaponStats weaponStats, WeaponStatsUpgradeTypeEnum upgradeType)
        {
            return GetWeaponStatsProgressionPerUpgrationLevel(weaponType).GetPrice(weaponStats, upgradeType);
        }

        public int GetUpgradeLevel(WeaponType weaponType, WeaponStatsUpgradeTypeEnum statsType, WeaponStats weaponStats)
        {
            return GetWeaponStatsProgressionPerUpgrationLevel(weaponType).GetUpgradeLevel(statsType, weaponStats);
        }

        public string GetUpgradeValue(WeaponType weaponType, WeaponStatsUpgradeTypeEnum statsType, int upgradeLevel)
        {
            return GetWeaponStatsProgressionPerUpgrationLevel(weaponType).GetUpgradeValue(statsType, upgradeLevel);
        }

        private WeaponStatsProgressionPerUpgrationLevel GetWeaponStatsProgressionPerUpgrationLevel(WeaponType weaponType)
        {
            foreach (WeaponStatsProgressionPerUpgrationLevel weaponStatsProgressionPerUpgrationLevel in weaponStatsProgressionPerUpgrationLevels)
            {
                if (weaponStatsProgressionPerUpgrationLevel.weaponType == weaponType)
                {
                    return weaponStatsProgressionPerUpgrationLevel;
                }
            }

            return new WeaponStatsProgressionPerUpgrationLevel();
        }
    }
}
