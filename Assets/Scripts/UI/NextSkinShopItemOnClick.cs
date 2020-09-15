using System.Collections;
using System.Collections.Generic;
using TDS_MG.Shop;
using UnityEngine;
using UnityEngine.UI;

namespace TDS_MG.UI
{
    [RequireComponent(typeof(Button))]
    public class NextSkinShopItemOnClick : MonoBehaviour
    {
        Button button;
        SkinShop skinShop;

        private void Awake()
        {
            button = GetComponent<Button>();
            skinShop = FindObjectOfType<SkinShop>();
        }

        private void Start()
        {
            button.onClick.AddListener(() => skinShop.NextSkinShopItem());
        }
    }
}
