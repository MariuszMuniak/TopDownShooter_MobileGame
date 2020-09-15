using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

namespace TDS_MG.UI
{
    public class CharacterSkinShopPanel : MonoBehaviour
    {
        [SerializeField] TextMeshProUGUI priceText = null;
        [SerializeField] GameObject selectButton = null;
        [SerializeField] GameObject buyButton = null;

        public void ShowCorrectButtonsAndPrice(bool isOwnedSelectedSkin, string price)
        {
            if (isOwnedSelectedSkin)
            {
                HideBuyButton();
                ShowSelectButton();
                HidePriceLabel();
            }
            else
            {
                HideSelectButton();
                ShowBuyButton();
                ShowAndSetUpPrice(price);
            }
        }

        private void HideBuyButton() => buyButton.SetActive(false);

        private void ShowSelectButton() => selectButton.SetActive(true);

        private void HidePriceLabel() => priceText.rectTransform.parent.gameObject.SetActive(false);

        private void HideSelectButton() => selectButton.SetActive(false);

        private void ShowBuyButton() => buyButton.SetActive(true);

        private void ShowAndSetUpPrice(string price)
        {
            SetUpPriceText(price);
            ShowPriceLabel();
        }

        private void SetUpPriceText(string price) => priceText.text = price;

        private void ShowPriceLabel() => priceText.rectTransform.parent.gameObject.SetActive(true);

        public void HideButtonsAndPrice()
        {
            HideBuyButton();
            HidePriceLabel();
            HideSelectButton();
        }
    }
}
