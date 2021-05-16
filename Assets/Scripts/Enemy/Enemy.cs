using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using General;
using Player;
using TMPro;
using UnityEngine;

namespace Assets.Scripts
{
    public class Enemy : Damageable
    {
        [SerializeField] private float _damage;
        [SerializeField] private float _maxSpeed;
        [SerializeField] private float _attackRange;
        [SerializeField] private float _attackDelay;
        [SerializeField] private LayerMask _playerSideMask;
        private float _attackCooldown;

        private void FixedUpdate()
        {
            if (_attackCooldown <= 0)
            {
                TryToDamage();
                _attackCooldown = _attackDelay;
            }
            else _attackCooldown -= Time.deltaTime;
        }

        private void TryToDamage()
        {
            Collider2D target =
                Physics2D.OverlapCircle(transform.position, _attackRange, _playerSideMask);
            if (target != null) 
                target.GetComponent<Damageable>().TakeDamage(_damage);
        }

        void OnDrawGizmosSelected()
        {
            Gizmos.DrawWireSphere(transform.position, _attackRange);
        }
    }
}