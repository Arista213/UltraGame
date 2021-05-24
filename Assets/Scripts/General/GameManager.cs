using System;
using UnityEngine;
using UnityEngine.UI;

namespace General
{
    public class GameManager : MonoBehaviour
    {
        [SerializeField] private LayerMask _playerSideLayer;
        [SerializeField] private LayerMask _solidLayer;
        [SerializeField] private Text _moneyText;
        [SerializeField] private int _initialMoney;

        private void Awake()
        {
            var map = new Map(_solidLayer, _playerSideLayer);
            var resources = new Resource(_initialMoney, _moneyText);
        }

        private void FixedUpdate()
        {
            if (Map.EnemiesAlive == 0) 
                print("VICTORY!");
        }
    }
}