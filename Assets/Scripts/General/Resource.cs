using System;
using UnityEngine;
using UnityEngine.UI;

namespace General
{
    public class Resource : MonoBehaviour
    {
        [SerializeField] private Text _moneyText;
        public static int Money { get; private set; }

        private void Start()
        {
            Money = 30;
        }

        public static void GainMoneyForKill()
        {
            Money += 5;
        }

        private void UpdateMoneyStatus()
        {
            _moneyText.text = Money.ToString();
        }
    }
}