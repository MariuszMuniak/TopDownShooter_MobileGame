using System.Collections;
using System.Collections.Generic;
using TDS_MG.SceneManagement;
using UnityEngine;
using UnityEngine.UI;

namespace TDS_MG.UI
{
    public class SaveAndPlayOnClick : MonoBehaviour
    {
        private void Start()
        {
            SavingWrapper savingWrapper = FindObjectOfType<SavingWrapper>();
            GetComponent<Button>().onClick.AddListener(() => savingWrapper.SaveAndLoadScene(1));
        }
    }
}
