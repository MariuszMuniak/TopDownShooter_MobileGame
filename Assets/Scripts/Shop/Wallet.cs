using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace TDS_MG.Shop
{
    public class Wallet : MonoBehaviour
    {
        [SerializeField] int moneyOwned = 100;

        public void AddMoney(int value)
        {
            moneyOwned += Mathf.Abs(value);
        }

        public void SpendMoney(int value)
        {
            if (HaveEnoughMoney(value))
            {
                moneyOwned -= Mathf.Abs(value);
            }
        }

        public int GetMoney()
        {
            return moneyOwned;
        }

        public bool HaveEnoughMoney(int cost)
        {
            return moneyOwned - Mathf.Abs(cost) >= 0;
        }
    }
}
