using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Assets.Scripts
{
    public class Turret : MonoBehaviour
    {
        [Header("Attributes")]
        [SerializeField] protected float MaxHealth = 20f;
        [SerializeField] protected float Range = 2f;
        [SerializeField] protected float Damage = 5f;
        [SerializeField] protected float FireRate = 1f;
        protected float FireCountdown = 0f;

        [Header("Unity Setup Fields")] [SerializeField]
        protected Transform Target;

        [SerializeField] protected Transform Cannon;

        [SerializeField] protected LayerMask Enemy;

        [SerializeField] protected GameObject Bullet;


        void Start()
        {
            InvokeRepeating("UpdateTarget", 0f, 0.5f);
        }

        void UpdateTarget()
        {
            Collider2D enemy = Physics2D.OverlapCircle(transform.position, Range, Enemy);
            Target = enemy != null ? enemy.transform : null;
        }


        void FixedUpdate()
        {
            if (Target == null)
            {
                FireCountdown -= Time.deltaTime;
                return;
            }

            if (FireCountdown <= 0)
            {
                Shoot();
                FireCountdown = FireRate;
            }
            else
                FireCountdown -= Time.deltaTime;
        }

        protected void Shoot()
        {
            GameObject bulletGo = Instantiate(Bullet, Cannon.position, Cannon.rotation);
            Bullet bullet = bulletGo.GetComponent<Bullet>();
            if (bullet != null)
            {
                bullet.Seek(Target, Damage);
            }
        }

        void TakeDamage(float damage)
        {
            MaxHealth -= damage;
            if (MaxHealth <= damage)
                Destroy(gameObject);
        }

        void OnDrawGizmos()
        {
            Gizmos.color = Color.blue;
            Gizmos.DrawWireSphere(transform.position, Range);
        }
    }
}