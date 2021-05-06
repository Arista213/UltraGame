using System;
using System.Collections;
using System.Collections.Generic;
using Assets.Scripts;
using Assets.Scripts.Player;
using UnityEngine;

namespace Assets.Scripts
{
    public class HandPosScript : MonoBehaviour
    {
        [SerializeField] private const float _handRange = 0.2f;
        private Vector3 _verticalHandPos = new Vector3(0f, _handRange, 0f);
        private Vector3 _horizontalHandPos = new Vector3(_handRange, 0f, 0f);
        private const float ValueOfChangeHand = 0.1f;
        [SerializeField] private Transform _playerTransform;
        [NonSerialized] public Direction HandDirection;

        void FixedUpdate()
        {
            if (Input.GetAxis("Vertical") >= ValueOfChangeHand && Input.GetKey(KeyCode.W))
            {
                transform.position = _playerTransform.position + _verticalHandPos;
                HandDirection = Direction.Up;
            }
            else if (Input.GetAxis("Vertical") <= -ValueOfChangeHand && Input.GetKey(KeyCode.S))
            {
                transform.position = _playerTransform.position - _verticalHandPos;
                HandDirection = Direction.Down;
            }
            else if (Input.GetAxis("Horizontal") <= -ValueOfChangeHand && Input.GetKey(KeyCode.A))
            {
                transform.position = _playerTransform.position - _horizontalHandPos;
                HandDirection = Direction.Left;
            }
            else if (Input.GetAxis("Horizontal") >= ValueOfChangeHand && Input.GetKey(KeyCode.D))
            {
                transform.position = _playerTransform.position + _horizontalHandPos;
                HandDirection = Direction.Right;
            }
        }
    }
}