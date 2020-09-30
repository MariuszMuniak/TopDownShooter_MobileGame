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
            float healthPercentage = health.GetHealthPercentage();

            if (healthPercentage <= 0)
            {
                HideHealthBar();
            }
            else
            {
                ShowHealthBar();
            }

            filledImage.fillAmount = healthPercentage;
        }

        private void HideHealthBar()
        {
            foreach (Image image in GetImagesFromChildren())
            {
                image.enabled = false;
            }
        }

        private void ShowHealthBar()
        {
            foreach (Image image in GetImagesFromChildren())
            {
                image.enabled = true;
            }
        }

        private Image[] GetImagesFromChildren()
        {
            return GetComponentsInChildren<Image>();
        }
    }
}
