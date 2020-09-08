using System.Collections;
using System.Collections.Generic;
using TDS_MG.Core;
using UnityEngine;
using UnityEngine.UI;

namespace TDS_MG.UI
{
    [RequireComponent(typeof(Button))]
    public class MainMenuPanelOnClickOpener : MonoBehaviour
    {
        [SerializeField] MainMenuPanelType panelType;
        [SerializeField] bool closeOpenedPanelOnClick = true;


        void Start()
        {
            Button button = GetComponent<Button>();
            MainMenuManager mainMenuManager = FindObjectOfType<MainMenuManager>();

            if (closeOpenedPanelOnClick)
            {
                button.onClick.AddListener(() => mainMenuManager.CloseOpenedPanel());
            }

            button.onClick.AddListener(() => mainMenuManager.OpenPanel(panelType));
        }
    }
}
