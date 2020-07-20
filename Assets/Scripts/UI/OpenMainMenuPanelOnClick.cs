using System.Collections;
using System.Collections.Generic;
using TDS_MG.Core;
using UnityEngine;
using UnityEngine.UI;

namespace TDS_MG.UI
{
    public class OpenMainMenuPanelOnClick : MonoBehaviour
    {
        [SerializeField] MainMenuPanelType panelType;

        void Start()
        {
            GetComponent<Button>().onClick.AddListener(() => FindObjectOfType<MainMenuManager>().OpenPanel(panelType));
        }
    }
}
