using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace TDS_MG.UI
{
    public class GameplayPanelsMenager : MonoBehaviour
    {
        [SerializeField] GameObject gui = null;
        [SerializeField] GameObject victoryPanel = null;
        [SerializeField] GameObject defeatPanel = null;

        private void Start()
        {
            ShowOnlyGUI();
        }

        public void ShowOnlyGUI()
        {
            DeactivatePanels();
            ShowGUI();
        }

        private void DeactivatePanels()
        {
            victoryPanel.SetActive(false);
            defeatPanel.SetActive(false);
        }

        private void ShowGUI()
        {
            gui.SetActive(true);
        }

        public void ShowOnlyVictoryPanel()
        {
            DeactivatePanels();
            HideGUI();
            ShowVictoryPanel();
        }

        private void HideGUI()
        {
            gui.SetActive(false);
        }

        private void ShowVictoryPanel()
        {
            victoryPanel.SetActive(true);
        }

        public void ShowOnlyDefeatPanel()
        {
            DeactivatePanels();
            HideGUI();
            ShowDefeatPanel();
        }

        private void ShowDefeatPanel()
        {
            defeatPanel.SetActive(true);
        }
    }
}
