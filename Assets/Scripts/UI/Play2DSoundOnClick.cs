using System.Collections;
using System.Collections.Generic;
using TDS_MG.Audio;
using UnityEngine;
using UnityEngine.UI;

namespace TDS_MG.UI
{
    public class Play2DSoundOnClick : MonoBehaviour
    {
        private void Awake()
        {
            Audio2DPlayer audio2DPlayer = FindObjectsOfType<Audio2DPlayer>()[0];
            GetComponent<Button>().onClick.AddListener(() => audio2DPlayer.PlayOneShot());
        }
    }
}
