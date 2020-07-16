using System.Collections;
using System.Collections.Generic;
using TDS_MG.Combat;
using UnityEngine;
using UnityEngine.UI;

namespace TDS_MG.UI
{
    public class ReloadWeaponOnClick : MonoBehaviour
    {
        private void Start()
        {
            GetComponent<Button>().onClick.AddListener(() => GameObject.FindWithTag("Player").GetComponent<PlayerFighter>().Reload());
        }
    }
}
