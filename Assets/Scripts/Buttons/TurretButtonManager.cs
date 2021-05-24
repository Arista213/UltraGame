using System;
using Turrets;
using UnityEngine;
using UnityEngine.UI;

namespace Buttons
{
    public class TurretButtonManager : MonoBehaviour
    {
        [SerializeField] protected GameObject TurretPrefab;
        [SerializeField] private Text PriceText;

        private void Start()
        {
            PriceText.text = "$" +  GetComponent<Turret>().BuildPrice;
        }

        public void SetTurret()
        {
            BuildManager.SetTurretToBuild(TurretPrefab);
        }
    }
}