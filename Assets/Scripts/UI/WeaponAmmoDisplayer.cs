using System.Collections;
using System.Collections.Generic;
using TDS_MG.Combat;
using TMPro;
using UnityEngine;

namespace TDS_MG.UI
{
    public class WeaponAmmoDisplayer : MonoBehaviour
    {
        TextMeshProUGUI textMesh;
        PlayerFighter playerFighter;

        private void Awake()
        {
            textMesh = GetComponentInChildren<TextMeshProUGUI>();
            playerFighter = GameObject.FindWithTag("Player").GetComponent<PlayerFighter>();
        }

        private void Update()
        {
            textMesh.text = $"{playerFighter.CurrentAmmoInMagazine()} / {playerFighter.MagazineSize()}";
        }
    }
}
