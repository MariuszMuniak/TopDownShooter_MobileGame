using System.Collections;
using System.Collections.Generic;
using TDS_MG.Control;
using TDS_MG.SceneManagement;
using UnityEngine;
using UnityEngine.UI;

namespace TDS_MG.UI
{
    public class LevelButton : MonoBehaviour
    {
        [SerializeField] int sceneBuildIndex = 0;

        Button button;

        private void Awake()
        {
            button = GetComponent<Button>();
        }

        void Start()
        {
            button.interactable = FindObjectOfType<LevelController>().IsLevelComplished(sceneBuildIndex);
            button.onClick.AddListener(() => FindObjectOfType<SavingWrapper>().SaveAndLoadScene(sceneBuildIndex));
        }
    } 
}
