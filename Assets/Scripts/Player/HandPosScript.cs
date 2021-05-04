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
        private const float ValueOfChangeHand = 0.1f;
        [SerializeField] private Transform _playerTransform;
        [NonSerialized] public Direction HandDirection;
        private Vector3 _currentHandPosition;

        private void Start()
        {
            _currentHandPosition = transform.position - _playerTransform.position;
            print(_currentHandPosition);
        }

        private void FixedUpdate()
        {
            if (Input.GetAxis("Vertical") >= ValueOfChangeHand && Input.GetKey(KeyCode.W))
            {
                transform.position = _playerTransform.position + new Vector3(_currentHandPosition.y, _currentHandPosition.x, _currentHandPosition.z);
                HandDirection = Direction.Up;
            }
            else if (Input.GetAxis("Vertical") <= -ValueOfChangeHand && Input.GetKey(KeyCode.S))
            {
                transform.position = _playerTransform.position - new Vector3(_currentHandPosition.y, _currentHandPosition.x, _currentHandPosition.z);
                HandDirection = Direction.Down;
            }
            else if (Input.GetAxis("Horizontal") <= -ValueOfChangeHand && Input.GetKey(KeyCode.A))
            {
                transform.position = _playerTransform.position - _currentHandPosition;
                HandDirection = Direction.Left;
            }
            else if (Input.GetAxis("Horizontal") >= ValueOfChangeHand && Input.GetKey(KeyCode.D))
            {
                transform.position = _playerTransform.position + _currentHandPosition;
                HandDirection = Direction.Right;
            }
        }
    }
}