using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace TDS_MG.Core
{
    public class MainMenuManager : MonoBehaviour
    {
        [SerializeField] GameObject[] panels = new GameObject[0];

        GameObject openedPanel = null;

        private void Start()
        {
            OpenPanel(MainMenuPanelType.MainPanel);
        }

        public void OpenPanel(MainMenuPanelType panelType)
        {
            int index = (int)panelType;

            if (index < panels.Length)
            {
                openedPanel = panels[index];
                openedPanel.SetActive(true);
            }
        }

        public void CloseOpenedPanel()
        {
            if (openedPanel != null)
            {
                openedPanel.SetActive(false);
            }
        }
    }
}
