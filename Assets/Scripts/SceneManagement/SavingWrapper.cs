using System;
using System.Collections;
using System.Collections.Generic;
using TDS_MG.Saving;
using UnityEngine;

namespace TDS_MG.SceneManagement
{
    public class SavingWrapper : MonoBehaviour
    {
        [SerializeField] float fadeInTime = 0.2f;

        const string defaultSaveFile = "save";

        private void Start()
        {
            Load();
        }

        private IEnumerator LoadLastScene()
        {
            yield return GetComponent<SavingSystem>().LoadLastScene(defaultSaveFile);

            Fader fader = FindObjectOfType<Fader>();
            fader.FadeOutImmediate();

            yield return fader.FadeIn(fadeInTime);
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
            GetComponent<SavingSystem>().Save(defaultSaveFile);
        }

        public void Load()
        {
            GetComponent<SavingSystem>().Load(defaultSaveFile);
        }

        public void Delete()
        {
            GetComponent<SavingSystem>().Delete(defaultSaveFile);
        }
    }
}
