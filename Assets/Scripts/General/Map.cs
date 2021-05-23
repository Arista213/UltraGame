using System;
using System.Collections.Generic;
using System.Linq;
using Enemy;
using UnityEngine;

namespace General
{
    public class Map : MonoBehaviour
    {
        public static List<Vector3> PlayerSideTransforms { get; private set; }
        public static PathFinder PathFinder { get; set; }
        [SerializeField] private LayerMask _solidLayer;

        private void Awake()
        {
            PlayerSideTransforms = new List<Vector3>();
            PathFinder = new PathFinder(_solidLayer, GameObject.FindWithTag("Player").transform);
        }

        public static void Add(Vector3 position)
        {
            PlayerSideTransforms.Add(position);
        }

        public static void Remove(Vector3 position)
        {
            PlayerSideTransforms.Remove(position);
        }
    }
}