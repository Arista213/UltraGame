using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Turrets
{
    
    public class TurretPlacing : MonoBehaviour
    {
        public static TurretPlacing instance;

        private GameObject turrentToBuild;
        public GameObject standartTurretPrefab;

        private void Start()
        {
            turrentToBuild = standartTurretPrefab;
        }

        private void Awake()
        {
            if (instance != null)
            {
                Debug.Log("no");
            }
            instance = this;
        }

        public GameObject GetTurrentToBuild()
        {
            return turrentToBuild;
        }
    }
}
