using System;
using System.Collections;
using System.Collections.Generic;
using TDS_MG.Saving;
using UnityEngine;
using UnityEngine.SceneManagement;

namespace TDS_MG.SceneManagement
{
    public class SavingWrapper : MonoBehaviour
    {
        [SerializeField] float fadeInTime = 0.2f;

        const string DEFAULT_SAVE_FILE = "save";

        private void Awake()
        {
            Load();
        }

        void Update()
        {
            InputsInDebugBuild();
        }

        private void InputsInDebugBuild()
        {
            if (Debug.isDebugBuild)
            {
                if (Input.GetKeyDown(KeyCode.L))
                {
                    Load();
                }

                if (Input.GetKeyDown(KeyCode.S))
                {
                    Save();
                }

                if (Input.GetKeyDown(KeyCode.Delete))
                {
                    Delete();
                }
            }
        }

        public void Save()
        {
            GetComponent<SavingSystem>().Save(DEFAULT_SAVE_FILE);
        }

        public void Load()
        {
            GetComponent<SavingSystem>().Load(DEFAULT_SAVE_FILE);
        }

        public void Delete()
        {
            GetComponent<SavingSystem>().Delete(DEFAULT_SAVE_FILE);
        }

        public void SaveAndLoadScene(int sceneBuildIndex)
        {
            GetComponent<SavingSystem>().Save(DEFAULT_SAVE_FILE);
            StartCoroutine(LoadScene(sceneBuildIndex));
        }

        private IEnumerator LoadScene(int sceneBuildIndex)
        {
            yield return FindObjectOfType<Fader>().FadeOut(fadeInTime);
            SceneManager.LoadScene(sceneBuildIndex);
        }

        private IEnumerator LoadLastScene()
        {
            yield return GetComponent<SavingSystem>().LoadLastScene(DEFAULT_SAVE_FILE);

            Fader fader = FindObjectOfType<Fader>();
            fader.FadeOutImmediate();

            yield return fader.FadeIn(fadeInTime);
        }
    }
}
