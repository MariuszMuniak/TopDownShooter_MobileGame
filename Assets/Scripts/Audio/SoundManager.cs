using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Audio;

namespace TDS_MG.Audio
{
    public class SoundManager : MonoBehaviour
    {
        [SerializeField] AudioMixer audioMixer = null;
        [SerializeField] [Range(-80, 0)] int customlowestDecibels = -20;

        const string MASTER_CHANNEL = "masterVol";
        const string MUSIC_CHANNEL = "musicVol";
        const int LOWEST_DECIBELS = -80;

        public void SetValue(AudioChanel chanel, float value)
        {
            float decibels = customlowestDecibels + (customlowestDecibels * -value);

            if (value == 0)
            {
                decibels = LOWEST_DECIBELS;
            }

            switch (chanel)
            {
                case AudioChanel.Master:
                    audioMixer.SetFloat(MASTER_CHANNEL, decibels);
                    break;
                case AudioChanel.Music:
                    audioMixer.SetFloat(MUSIC_CHANNEL, decibels);
                    break;
            }
        }
    }
}
