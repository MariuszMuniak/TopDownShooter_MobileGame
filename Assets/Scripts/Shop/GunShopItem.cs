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
        [SerializeField] WeaponType weaponType = WeaponType.NoWeapon;
        [SerializeField] Button button = null;
        [SerializeField] int price = 0;

        public bool IsOwned() => isOwned;

        public WeaponType GetWeaponType()
        {
            return weaponType;
        }

        public Image GetButtonImage()
        {
            return button.image;
        }

        public Image GetButtonChildrenImage()
        {
            Image[] images = button.GetComponentsInChildren<Image>();

            foreach (Image image in images)
            {
                if (image != button.image)
                {
                    return image;
                }
            }

            return button.image;
        }

        public int GetPrice() => price;
    }
}
