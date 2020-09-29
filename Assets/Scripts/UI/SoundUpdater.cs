using System.Collections;
using System.Collections.Generic;
using TDS_MG.Audio;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

namespace TDS_MG.UI
{
    public class SoundUpdater : MonoBehaviour
    {
        [SerializeField] AudioChanel chanel;
        [SerializeField] Slider slider = null;
        [SerializeField] TextMeshProUGUI textMesh = null;

        SoundManager soundManager;

        private void Awake()
        {
            soundManager = FindObjectOfType<SoundManager>();
        }

        private void Start()
        {
            slider.onValueChanged.AddListener(delegate { UpdateSoundLevel(); });
            UpdateSoundLevel();
        }

        private void UpdateSoundLevel()
        {
            if (soundManager == null)
            {
                return;
            }

            int sliderValue = (int)(slider.value * 100);

            textMesh.text = $"{chanel}: {sliderValue}/100";
            soundManager.SetValue(chanel, slider.value);
        }
    }
}
