using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace TDS_MG.Audio
{
    [RequireComponent(typeof(AudioSource))]
    public class Audio2DPlayer : MonoBehaviour
    {
        AudioSource audioSource;

        private void Awake()
        {
            audioSource = GetComponent<AudioSource>();
        }

        private void Start()
        {
            audioSource.spatialBlend = 0f;
        }

        public void PlayOneShot()
        {
            if (audioSource.clip != null)
            {
                audioSource.PlayOneShot(audioSource.clip);
            }
        }
    }
}
