using System;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

namespace General
{
    public class Map : MonoBehaviour
    {
        private void Awake()
        {
            PlayerSideTransforms = new List<Vector3>();
        }
        public static List<Vector3> PlayerSideTransforms { get; private set; }

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