// using System;
// using General;
// using UnityEngine;
//
//
// public enum Side
// {
//     Player,
//     Enemy
// }
//
// public class Creation : MonoBehaviour
// {
//     public void Instantiate(GameObject gameObject,
//         Vector3 createPointPosition, Quaternion createPointRotation)
//     {
//         Instantiate(gameObject, createPointPosition, createPointRotation);
//     }
// }
//
// public static class Factory
// {
//     private static Creation _creation = new Creation();
//
//     public static void Create(GameObject gameObject, Transform createPoint, Side side)
//     {
//         if (side == Side.Player)
//             Map.PlayerSideTransforms.Add(gameObject.transform);
//         _creation.Instantiate(gameObject, createPoint.position, createPoint.rotation);
//     }
// }