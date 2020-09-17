using System.Collections;
using System.Collections.Generic;
using TDS_MG.Character;
using TDS_MG.Saving;
using TDS_MG.SceneManagement;
using TDS_MG.UI;
using TMPro;
using UnityEngine;

namespace TDS_MG.Shop
{
    public class SkinShop : MonoBehaviour, ISaveable
    {
        [SerializeField] CharacterSkinShopPanel skinShopPanel = null;
        [SerializeField] SkinShopItem[] skinShopItems = new SkinShopItem[22];

        PlayerSkinCollection playerSkinCollection;
        Wallet wallet;
        SavingWrapper savingWrapper;
        int currentSkinIndex = 0;
        int selectedSkinIndex = 0;

        private void Awake()
        {
            playerSkinCollection = FindObjectOfType<PlayerSkinCollection>();
            wallet = FindObjectOfType<Wallet>();
            savingWrapper = FindObjectOfType<SavingWrapper>();
        }

        private void Start()
        {
            currentSkinIndex = playerSkinCollection.GetActiveSkinIndex();
            selectedSkinIndex = currentSkinIndex;
        }

        public void BuySkin()
        {
            if (CanBuy())
            {
                playerSkinCollection.TakePossession(skinShopItems[selectedSkinIndex].skinType);
                SelectSkin();
                wallet.SpendMoney(skinShopItems[selectedSkinIndex].price);
                savingWrapper.Save();
            }
        }

        private bool CanBuy() => wallet.HaveEnoughMoney(skinShopItems[selectedSkinIndex].price);

        public void SelectSkin()
        {
            currentSkinIndex = selectedSkinIndex;
            ShowCorrectButtonsAndPrice();
        }

        private void ShowCorrectButtonsAndPrice()
        {
            if (currentSkinIndex == selectedSkinIndex)
            {
                skinShopPanel.HideButtonsAndPrice();
            }
            else
            {
                ShowButtonsAndPrice();
            }
        }

        private void ShowButtonsAndPrice()
        {
            SkinShopItem skinShopItem = skinShopItems[selectedSkinIndex];
            bool isOwned = playerSkinCollection.IsOwned(skinShopItem.skinType);
            string price = skinShopItem.price.ToString();

            skinShopPanel.ShowCorrectButtonsAndPrice(isOwned, price);
        }

        public void ShowCurrentSkin()
        {
            ShowSkinShopItem(currentSkinIndex);
            selectedSkinIndex = currentSkinIndex;
            ShowCorrectButtonsAndPrice();
        }

        public void NextSkinShopItem()
        {
            NextSelectedSkinIndex();
            ShowSkinShopItem(selectedSkinIndex);
            ShowCorrectButtonsAndPrice();
        }

        private void NextSelectedSkinIndex()
        {
            selectedSkinIndex++;

            if (selectedSkinIndex == skinShopItems.Length)
            {
                selectedSkinIndex = 0;
            }
        }

        private void ShowSkinShopItem(int index)
        {
            playerSkinCollection.Activate(skinShopItems[index].skinType);
        }

        public void PreviousSkinShopItem()
        {
            PreviousSelectedSkinIndex();
            ShowSkinShopItem(selectedSkinIndex);
            ShowCorrectButtonsAndPrice();
        }

        private void PreviousSelectedSkinIndex()
        {
            selectedSkinIndex--;

            if (selectedSkinIndex < 0)
            {
                selectedSkinIndex = skinShopItems.Length - 1;
            }
        }

        public object CaptureState()
        {
            return currentSkinIndex;
        }

        public void RestoreState(object state)
        {
            currentSkinIndex = (int)state;
        }

        [System.Serializable]
        private class SkinShopItem
        {
            public PlayerSkinType skinType;
            public int price;
        }
    }
}
