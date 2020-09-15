using System.Collections;
using System.Collections.Generic;
using TDS_MG.Shop;
using UnityEngine;

namespace TDS_MG.UI
{
    public class CurrentSkinPresenter : MonoBehaviour
    {
        [SerializeField] bool onEnable = false;
        [SerializeField] bool onDisable = false;

        SkinShop skinShop;

        private void Awake()
        {
            skinShop = FindObjectOfType<SkinShop>();
        }

        private void OnEnable()
        {
            if (onEnable)
            {
                skinShop.ShowCurrentSkin();
            }
        }

        private void OnDisable()
        {
            if (onDisable)
            {
                skinShop.ShowCurrentSkin();
            }
        }
    }
}
