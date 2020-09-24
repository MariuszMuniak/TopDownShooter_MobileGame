using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

namespace TDS_MG.UI
{
    public class ReloadSceneOnClick : MonoBehaviour
    {
        void Start()
        {
            GetComponent<Button>().onClick.AddListener(() => LoadCurrentScene());
        }

        private void LoadCurrentScene()
        {
            SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
        }
    }
}
