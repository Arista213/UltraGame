using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Assets.Scripts
{
    public class Enemy: MonoBehaviour
    {
        [SerializeField] private float _hitpoints;
        [SerializeField] private float _damage;
        [SerializeField] private float _maxSpeed;
        [SerializeField] private float _attackRange;
        [SerializeField] private float _attackDelay;

        void TakeDamage(float damage)
        {
            _hitpoints -= damage;
        }

        void FixedUpdate()
        {
            var playerPosition = GameObject.FindGameObjectWithTag("Player").transform.position;
            Vector2 direction = (playerPosition - transform.position).normalized;
            GetComponent<Rigidbody2D>().velocity = direction * _maxSpeed;  
        }
    }
}