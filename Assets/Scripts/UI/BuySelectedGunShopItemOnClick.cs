﻿using System.Collections;
using System.Collections.Generic;
using TDS_MG.Shop;
using UnityEngine;
using UnityEngine.UI;

namespace TDS_MG.UI
{
    [RequireComponent(typeof(Button))]
    public class BuySelectedGunShopItemOnClick : MonoBehaviour
    {
        Button button;
        GunShop gunShop;

        private void Awake()
        {
            button = GetComponent<Button>();
            gunShop = FindObjectOfType<GunShop>();
        }

        private void Start()
        {
            button.onClick.AddListener(() => gunShop.BuySelectedGunShopItem());
        }
    }
}