using System;
using System.Collections;
using System.Collections.Generic;
using Assets.Scripts;
using UnityEngine;

namespace Assets.Scripts
{
    public enum Direction
    {
        Up,
        Down,
        Left,
        Right
    }

    public class HandPosScript : MonoBehaviour
    {
        [SerializeField] private readonly Vector3 _verticalHandPos = new Vector3(0f, 0.8f, 0f);
        [SerializeField] private readonly Vector3 _horizontalHandPos = new Vector3(0.8f, 0.1f, 0f);
        [SerializeField] private const float ValueOfChangeHand = 0.6f;
        [SerializeField] private Transform _playerTransform;
        [NonSerialized] public Direction HandDirection;

        void FixedUpdate()
        {
            if (Input.GetAxis("Vertical") >= ValueOfChangeHand)
            {
                transform.position = _playerTransform.position + _verticalHandPos;
                HandDirection = Direction.Up;
            }
            else if (Input.GetAxis("Vertical") <= -ValueOfChangeHand)
            {
                transform.position = _playerTransform.position - _verticalHandPos;
                HandDirection = Direction.Down;
            }
            else if (Input.GetAxis("Horizontal") <= -ValueOfChangeHand)
            {
                transform.position = _playerTransform.position - _horizontalHandPos;
                HandDirection = Direction.Left;
            }
            else if (Input.GetAxis("Horizontal") >= ValueOfChangeHand)
            {
                transform.position = _playerTransform.position + _horizontalHandPos;
                HandDirection = Direction.Right;
            }
        }
    }
}