using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

namespace TDS_MG.UI
{
    public class LoadSceneOnClick : MonoBehaviour
    {
        [SerializeField] int sceneBuildIndex = 0;

        void Start()
        {
            GetComponent<Button>().onClick.AddListener(() => SceneManager.LoadScene(sceneBuildIndex));
        }
    }
}
