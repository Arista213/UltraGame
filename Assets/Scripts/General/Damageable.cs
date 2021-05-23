using System;
using System.Data;
using System.Linq;
using Turrets;
using UnityEngine;
using UnityEngine.UI;

namespace General
{
    public class Damageable : MonoBehaviour
    {
        [Header("Attributes")] [SerializeField]
        public float MaxHealth;

        public float Health;
        [Header("Unity stuff")] public Image healthBar;

        [SerializeField] protected GameObject DamageableGameobject;

        public void Awake()
        {
            Health = MaxHealth;
        }

        virtual public void TakeDamage(float damage)
        {
            Health -= damage;
            healthBar.fillAmount = Health / MaxHealth;
            if (Health <= 0)
            {
                Turret turret;
                if (gameObject.TryGetComponent(out turret))
                {
                    print(Map.PlayerSideTransforms.Count);
                    Map.Remove(gameObject.transform.position);
                }

                Destroy(DamageableGameobject);
            }
        }
    }
}