using System;
using System.Data;
using System.Linq;
using Turrets;
using UnityEngine;

namespace General
{
    public class Damageable : MonoBehaviour
    {
        [Header("Attributes")] [SerializeField]
        public float MaxHealth;

        public float Health;

        public void Awake()
        {
            Health = MaxHealth;
        }

        public void TakeDamage(float damage)
        {
            Health -= damage;
            if (Health <= 0)
            {
                Turret turret;
                if (gameObject.TryGetComponent(out turret))
                {
                    print(Map.PlayerSideTransforms.Count);
                    Map.Remove(gameObject.transform.position);
                }
                Destroy(gameObject);
            }
        }
    }
}