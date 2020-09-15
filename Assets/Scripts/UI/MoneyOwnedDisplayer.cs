using System.Collections;
using System.Collections.Generic;
using TDS_MG.Shop;
using TMPro;
using UnityEngine;

namespace TDS_MG.UI
{
    public class MoneyOwnedDisplayer : MonoBehaviour
    {
        Wallet wallet;
        TextMeshProUGUI textMeshPro;

        private void Awake()
        {
            wallet = GameObject.FindWithTag("Player").GetComponent<Wallet>();
            textMeshPro = GetComponentInChildren<TextMeshProUGUI>();
        }

        private void LateUpdate()
        {
            textMeshPro.text = wallet.GetMoney().ToString();
        }
    }
}
