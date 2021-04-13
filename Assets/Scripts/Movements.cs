using System;
using UnityEngine;
using UnityEngine.Animations;

namespace Assets.Scripts
{
    public class Movements : MonoBehaviour
    {
        [SerializeField] private float _mMaxSpeed = 3f;
        private bool isFacingRight = true;
        private Animator anim;

        private void Start()
        {
            anim = GetComponent<Animator>();
        }

        private void FixedUpdate()
        {
            float moveX = Input.GetAxis("Horizontal");
            float moveY = Input.GetAxis("Vertical");
            var speed = Math.Max(Mathf.Abs(moveX), Mathf.Abs(moveY));
            anim.SetFloat("Speed", speed);

            //обращаемся к компоненту персонажа RigidBody2D. задаем ему скорость по оси Х, 
            //равную значению оси Х умноженное на значение макс. скорости
            GetComponent<Rigidbody2D>().velocity =
                new Vector2(moveX * _mMaxSpeed, moveY * _mMaxSpeed);

            if (moveX > 0 && !isFacingRight)
                Flip();
            else if (moveX < 0 && isFacingRight)
                Flip();
        }

        private void Flip()
        {
            isFacingRight = !isFacingRight;
            Vector3 theScale = transform.localScale;
            theScale.x *= -1;
            transform.localScale = theScale;
        }
    }
}