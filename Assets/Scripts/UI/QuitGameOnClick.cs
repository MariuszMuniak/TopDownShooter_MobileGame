using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

namespace TDS_MG.UI
{
    public class QuitGameOnClick : MonoBehaviour
    {
        void Start()
        {
            GetComponent<Button>().onClick.AddListener(() => Application.Quit());
        }
    }
}
