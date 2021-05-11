using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

namespace Assets.Scripts
{
    public class Enemy : Damageable
    {
        [SerializeField] private float _damage;
        [SerializeField] private float _maxSpeed;
        [SerializeField] private float _attackRange;
        [SerializeField] private float _attackDelay;
    }
}