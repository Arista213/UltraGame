using Assets.Scripts;
using General;
using UnityEngine;

namespace Turrets
{
    public class Turret : Damageable
    {
        [Header("Attributes")] [SerializeField]
        protected float Range = 2f;

        [SerializeField] protected float Damage = 5f;
        [SerializeField] protected float FireRate = 1f;
        protected float FireCooldown = 0f;

        [Header("Unity Setup Fields")] [SerializeField]
        protected Transform Target;

        [SerializeField] protected Transform Cannon;

        [SerializeField] protected LayerMask Enemy;

        [SerializeField] protected GameObject Bullet;


        void Start()
        {
            Map.Add(transform.position);
            print(Map.PlayerSideTransforms.Count);
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
                FireCooldown -= Time.deltaTime;
                return;
            }

            if (FireCooldown <= 0)
            {
                Shoot();
                FireCooldown = FireRate;
            }
            else
                FireCooldown -= Time.deltaTime;
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

        void OnDrawGizmos()
        {
            Gizmos.color = Color.blue;
            Gizmos.DrawWireSphere(transform.position, Range);
        }
    }
}