using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

namespace TDS_MG.UI
{
    [RequireComponent(typeof(Button))]
    public class TestSceneOnClickLoader : MonoBehaviour
    {
        const string TEST_SCENE_NAME = "TestScene";

        Button button;

        private void Awake()
        {
            button = GetComponent<Button>();
        }

        void Start()
        {
            if (button != null)
            {
                button.onClick.AddListener(() => { SceneManager.LoadScene(TEST_SCENE_NAME); });
            }
        }
    }
}
