using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

namespace TDS_MG.Shop
{
    [System.Serializable]
    public class GunShopItem
    {
        public bool isOwned = false;
        public WeaponType weaponType = WeaponType.NoWeapon;
        public Button button = null;
        public int price = 0;

        public Image GetImageFromButton()
        {
            return button.image;
        }
    }
}
