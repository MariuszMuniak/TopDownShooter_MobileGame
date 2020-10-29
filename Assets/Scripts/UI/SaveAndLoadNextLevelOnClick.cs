using System.Collections;
using System.Collections.Generic;
using TDS_MG.Control;
using TDS_MG.SceneManagement;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

namespace TDS_MG.UI
{
    public class SaveAndLoadNextLevelOnClick : MonoBehaviour
    {
        void Start()
        {
            int nextSceneIndex = SceneManager.GetActiveScene().buildIndex + 1;
            nextSceneIndex = nextSceneIndex == FindObjectOfType<LevelController>().GetMaxSceneBuildIndex() ? 0 : nextSceneIndex;

            GetComponent<Button>().onClick.AddListener(() => FindObjectOfType<SavingWrapper>().SaveAndLoadScene(nextSceneIndex));
        }
    } 
}
