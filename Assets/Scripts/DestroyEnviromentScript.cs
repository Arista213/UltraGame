using System;
using System.Collections;
using System.Collections.Generic;
using System.Threading;
using UnityEngine;

namespace Assets.Scripts
{
    public class DestroyEnviromentScript : MonoBehaviour
    {
        private float _timeBtwDestroy;
        [SerializeField] private float _destroyDelay;
        [SerializeField] private Transform _destroyPos;
        [SerializeField] private LayerMask _objectToDestroy;
        [SerializeField] private Animator _anim;
        [SerializeField] private Vector3 _destroyRange;

        void FixedUpdate()
        {
            if (_timeBtwDestroy <= 0)
            {
                if (Input.GetKey(KeyCode.Space))
                {
                    _anim.SetTrigger("Destroy");
                    OnDestroy();
                    _timeBtwDestroy = _destroyDelay;
                }
            }
            else _timeBtwDestroy -= Time.deltaTime;
        }

        void OnDestroy()
        {
            Collider2D[] objectsToDestroy =
                Physics2D.OverlapBoxAll(_destroyPos.position, _destroyRange, 0f, _objectToDestroy);

            foreach (var e in objectsToDestroy)
                e.GetComponent<EnviromentDestroyable>().ToDestroy();
        }

        void OnDrawGizmosSelected()
        {
            Gizmos.DrawWireCube(_destroyPos.position, _destroyRange);
        }
    }
}