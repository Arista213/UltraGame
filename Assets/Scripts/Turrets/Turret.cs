using Assets.Scripts;
using General;
using UnityEngine;

namespace Turrets
{
    public class Turret : Damageable
    {
        [Header("Attributes")] [SerializeField]
        protected float Range = 2f;

        public float Damage = 5f;
        public float FireRate = 1f;
        public int BuildPrice = 100;
        protected float FireCooldown = 0f;

        [Header("Unity Setup Fields")] [SerializeField]
        protected Transform Target;

        public Transform Cannon;

        public LayerMask Enemy;

        public GameObject Bullet;
        public AudioSource AudioSource;
        public AudioClip ShootSound;


        void Start()
        {
            Map.Add(transform.position);
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

        public override void TakeDamage(float damage)
        {
            Health -= damage;
            healthBar.fillAmount = Health / MaxHealth;
            if (Health <= 0)
            {
                Map.Remove(transform.position);
                Destroy(DamageableGameobject);
            }
        }

        protected void Shoot()
        {
            AudioSource.PlayOneShot(ShootSound);
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