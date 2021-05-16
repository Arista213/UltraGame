using System;
using UnityEngine;

namespace Player
{
    public class PlayerMovements : MonoBehaviour
    {
        [SerializeField] private float _maxSpeed;
        private bool _isFacingRight = true;
        private Animator _anim;

        private void Start()
        {
            _anim = GetComponent<Animator>();
        }

        private void FixedUpdate()
        {
            float moveX = Input.GetAxis("Horizontal");
            float moveY = Input.GetAxis("Vertical");
            var speed = Math.Max(Mathf.Abs(moveX), Mathf.Abs(moveY));
            _anim.SetFloat("Speed", speed);
            
            GetComponent<Rigidbody2D>().velocity =
                new Vector2(moveX * _maxSpeed, moveY * _maxSpeed);

            if (moveX > 0 && !_isFacingRight)
                Flip();
            else if (moveX < 0 && _isFacingRight)
                Flip();
        }

        private void Flip()
        {
            _isFacingRight = !_isFacingRight;
            Vector3 theScale = transform.localScale;
            theScale.x *= -1;
            transform.localScale = theScale;
        }
    }
}