using System;
using UnityEngine;

namespace Player
{
    public class PlayerMovements : MonoBehaviour
    {
        [SerializeField] private float _maxSpeed;
        [SerializeField] private Transform _playerTransform;
        private bool _isFacingRight = true;
        [SerializeField] Animator _anim;
        [SerializeField] private Rigidbody2D _parantRigidbody2D;

        private void FixedUpdate()
        {
            float moveX = Input.GetAxis("Horizontal");
            float moveY = Input.GetAxis("Vertical");
            var speed = Math.Max(Mathf.Abs(moveX), Mathf.Abs(moveY));
            _anim.SetFloat("Speed", speed);
            
            _parantRigidbody2D.velocity =
                new Vector2(moveX * _maxSpeed, moveY * _maxSpeed);

            if (moveX > 0 && !_isFacingRight)
                Flip();
            else if (moveX < 0 && _isFacingRight)
                Flip();
        }

        private void Flip()
        {
            _isFacingRight = !_isFacingRight;
            Vector3 theScale = _playerTransform.localScale;
            theScale.x *= -1;
            _playerTransform.localScale = theScale;
        }
    }
}