using System.Collections;
using System.Collections.Generic;
using TDS_MG.SceneManagement;
using UnityEngine;
using UnityEngine.UI;

namespace TDS_MG.UI
{
    [RequireComponent(typeof(Button))]
    public class SaveAndBackToMainMenuOnClick : MonoBehaviour
    {
        private void Start()
        {
            GetComponent<Button>().onClick.AddListener(() => FindObjectOfType<SavingWrapper>().SaveAndLoadScene(0));
        }
    }
}
