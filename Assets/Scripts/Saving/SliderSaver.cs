using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

namespace TDS_MG.Saving
{
    [RequireComponent(typeof(Slider))]
    public class SliderSaver : MonoBehaviour, ISaveable
    {
        public object CaptureState()
        {
            return GetComponent<Slider>().value;
        }

        public void RestoreState(object state)
        {
            GetComponent<Slider>().value = (float)state;
        }
    }
}
