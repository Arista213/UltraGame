using System.Collections;
using System.Collections.Generic;
using General;
using UnityEngine;

namespace Assets.Scripts
{
    public class Bullet : MonoBehaviour
    {
        [SerializeField] private float _speed = 30f;
        private Transform _target;
        private float _damage;
        private float _rotationSpeed = 50f;

        public void Seek(Transform target, float damage)
        {
            _target = target;
            _damage = damage;
        }

        void FixedUpdate()
        {
            if (_target == null)
            {
                Destroy(gameObject);
                return;
            }

            RotateBullet();

            Vector3 dir = _target.position - transform.position;
            float distanceThisFrame = _speed * Time.deltaTime;

            if (dir.magnitude <= distanceThisFrame)
            {
                HitTarget();
                return;
            }

            transform.Translate(dir.normalized * distanceThisFrame, Space.World);
        }

        void HitTarget()
        {
            _target.gameObject.GetComponent<Damageable>().TakeDamage(_damage);
            Destroy(gameObject);
        }

        void RotateBullet()
        {
            Vector3 vectorToTarget = _target.position - transform.position;
            float angle = Mathf.Atan2(vectorToTarget.y, vectorToTarget.x) * Mathf.Rad2Deg;
            Quaternion q = Quaternion.AngleAxis(angle, Vector3.forward);
            transform.rotation = Quaternion.Slerp(transform.rotation, q, Time.deltaTime * _rotationSpeed);
        }
    }
}