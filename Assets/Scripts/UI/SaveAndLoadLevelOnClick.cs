using System.Collections;
using System.Collections.Generic;
using TDS_MG.SceneManagement;
using UnityEngine;
using UnityEngine.UI;

namespace TDS_MG.UI
{
    public class SaveAndLoadLevelOnClick : MonoBehaviour
    {
        [SerializeField] int sceneBuildIndex = 0;

        void Start()
        {
            GetComponent<Button>().onClick.AddListener(() => FindObjectOfType<SavingWrapper>().SaveAndLoadScene(sceneBuildIndex));
        }
    }
}
