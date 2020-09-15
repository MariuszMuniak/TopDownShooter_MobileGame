using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

namespace TDS_MG.UI
{
    public class GunShopItemBuyPanel : MonoBehaviour
    {
        [SerializeField] TextMeshProUGUI priceText = null;
        [SerializeField] Image selectedGunShopItemIcon = null;

        public void SetPrice(int price)
        {
            priceText.text = price.ToString();
        }

        public void SetSelectedGunShopItemIcon(Sprite icon)
        {
            selectedGunShopItemIcon.sprite = icon;
            selectedGunShopItemIcon.SetNativeSize();
        }
    }
}
