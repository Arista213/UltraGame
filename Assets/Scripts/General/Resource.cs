using System;
using UnityEngine;

namespace General
{
    public static class Resource
    {
        public static int Money { get; private set; }

        static Resource()
        {
            Money = 30;
        }

        public static void GainMoneyForKill()
        {
            Money += 5;
        }
    }
}