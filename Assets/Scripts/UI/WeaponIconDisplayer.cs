using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

namespace TDS_MG.UI
{
    public class WeaponIconDisplayer : MonoBehaviour
    {
        Image image;

        private void Awake()
        {
            image = GetComponent<Image>();
        }

        public void SetImage(Sprite sprite)
        {
            image.sprite = sprite;
            image.SetNativeSize();
        }
    }
}
