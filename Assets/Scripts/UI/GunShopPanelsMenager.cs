using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace TDS_MG.UI
{
    public class GunShopPanelsMenager : MonoBehaviour
    {
        [SerializeField] GameObject weaponUpgradePanel = null;
        [SerializeField] GameObject buyWeaponPanel = null;

        private void Start()
        {
            HidePanels();
        }

        public void HidePanels()
        {
            weaponUpgradePanel.SetActive(false);
            buyWeaponPanel.SetActive(false);
        }

        public void OnlyShowWeaponUpgradePanel()
        {
            HidePanels();
            weaponUpgradePanel.SetActive(true);
        }

        public void OnlyShowBuyWeaponPanel()
        {
            HidePanels();
            buyWeaponPanel.SetActive(true);
        }
    }
}
