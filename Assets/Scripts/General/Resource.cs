using System;
using UnityEngine;
using UnityEngine.UI;

namespace General
{
    public class Resource
    {
        private static int money;
        private static Text moneyText;

        public Resource(int _money, Text _moneyText)
        {
            money = _money;
            moneyText = _moneyText;
            UpdateMoneyStatus();
        }

        public static void BuildTower(int price)
        {
            money -= price;
            UpdateMoneyStatus();
        }

        public static void GainMoneyForKill()
        {
            money += 5;
            UpdateMoneyStatus();
        }

        private static void UpdateMoneyStatus()
        {
            moneyText.text = "$" + money;
        }
    }
}