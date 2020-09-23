using System.Collections;
using System.Collections.Generic;
using TDS_MG.Attributes;
using UnityEngine;
using UnityEngine.UI;

namespace TDS_MG.UI
{
    public class HealthBarDisplayer : MonoBehaviour
    {
        [SerializeField] Image filledImage;

        Health health;

        private void Awake()
        {
            health = GetComponentInParent<Health>();
        }

        private void Update()
        {
            filledImage.fillAmount = health.GetHealthPercentage();
        }
    }
}
