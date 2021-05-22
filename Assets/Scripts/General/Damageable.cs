        using System;
using System.Data;
using System.Linq;
using Enemy;
using Turrets;
using UnityEngine;

namespace General
{
    public class Damageable : MonoBehaviour
    {
        [Header("Attributes")] [SerializeField]
        public float Health;

        public virtual void TakeDamage(float damage)
        {
            Health -= damage;
            if (Health <= 0)
            {
                Turret turret;
                Player.Player player;
                if (gameObject.TryGetComponent(out turret))
                    Map.Remove(gameObject.transform.position);
                else if (gameObject.TryGetComponent(out player))
                    Enemy.Enemy.PlayerStatus = false;


                Destroy(gameObject);
            }
        }
    }
}