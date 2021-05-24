using System;
using System.Collections.Generic;
using System.Linq;
using Enemy;
using UnityEngine;
using UnityEngine.UI;

namespace General
{
    public class Map
    {
        public static List<Vector3> PlayerSideTransforms { get; private set; }
        public static PathFinder PathFinder { get; set; }
        public static int EnemiesAlive { get; set; }
        public static LayerMask PlayerSideLayer;
        public static LayerMask SolidLayer;

        public Map(LayerMask _solidLayer, LayerMask playerSideLayer)
        {
            PlayerSideTransforms = new List<Vector3>();
            PlayerSideLayer = playerSideLayer;
            SolidLayer = _solidLayer;
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