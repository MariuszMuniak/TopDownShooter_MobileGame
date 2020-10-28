using System.Collections;
using System.Collections.Generic;
using TDS_MG.Combat;
using UnityEngine;
using UnityEngine.UI;

namespace TDS_MG.UI
{
    public class ReloadBarDisplayer : MonoBehaviour
    {
        [SerializeField] Image image = null;

        PlayerFighter playerFighter;

        void Awake()
        {
            playerFighter = FindObjectOfType<PlayerFighter>();
        }

        void Update()
        {
            image.fillAmount = playerFighter.GetTimeLeftToEndReloading();
        }
    }
}
