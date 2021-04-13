using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Assets.Scripts
{
    public class EnvironmentDestroy : MonoBehaviour
    {
        private Transform player;
        private Vector2 hand;

        void Start()
        {
            player = GameObject.FindWithTag("Player").transform;
        }

        void UpdateHand()
        {
            var directionX = Input.GetAxis("Horizontal");
            var directionY = Input.GetAxis("Vertical");
            hand = new Vector2((float) Math.Ceiling(directionX), (float) Math.Ceiling(directionY));
        }

        void FixedUpdate()
        {
            UpdateHand();
        }
    }
}