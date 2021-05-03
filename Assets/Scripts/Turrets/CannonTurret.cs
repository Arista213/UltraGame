using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Assets.Scripts
{
    public class CannonTurret : Turret
    {
        private float _rotationSpeed = 3f;

        [SerializeField] private Transform _partToRotate;

        void RotateTower()
        {
            Vector3 vectorToTarget = Target.position - transform.position;
            float angle = Mathf.Atan2(vectorToTarget.y, vectorToTarget.x) * Mathf.Rad2Deg;
            Quaternion q = Quaternion.AngleAxis(angle, Vector3.forward);
            transform.rotation = Quaternion.Slerp(transform.rotation, q, Time.deltaTime * _rotationSpeed);
        }

        void FixedUpdate()
        {
            if (Target == null)
            {
                FireCountdown -= Time.deltaTime;
                return;
            }
            RotateTower();

            if (FireCountdown <= 0)
            {
                Shoot();
                FireCountdown = FireRate;
            }
            else
                FireCountdown -= Time.deltaTime;
        }
    }
}