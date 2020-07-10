using System.Collections;
using System.Collections.Generic;
using TDS_MG.Combat;
using UnityEngine;
using UnityEngine.UI;

namespace TDS_MG.UI
{
    public class PreviousWeaponOnClick : MonoBehaviour
    {
        PlayerFighter playerFighter;
        WeaponIconDisplayer iconDisplayer;

        private void Awake()
        {
            playerFighter = GameObject.FindWithTag("Player").GetComponent<PlayerFighter>();
            iconDisplayer = FindObjectOfType<WeaponIconDisplayer>();
        }

        void Start()
        {
            GetComponent<Button>().onClick.AddListener(() => PreviousWeapon());
        }

        private void PreviousWeapon()
        {
            Sprite sprite = playerFighter.PreviousWeapon();
            iconDisplayer.SetImage(sprite);
        }
    }
}
