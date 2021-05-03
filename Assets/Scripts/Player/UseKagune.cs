using System;
using System.Collections;
using System.Collections.Generic;
using System.Threading;
using UnityEngine;

namespace Assets.Scripts
{
    public class UseKagune : MonoBehaviour
    {
        private float _timeBtwDestroy;
        [SerializeField] private float _destroyDelay;
        [SerializeField] private LayerMask _destroyableEnviroment;
        [SerializeField] private LayerMask _enemy;
        [SerializeField] private Animator _anim;
        [SerializeField] private Vector3 _destroyRangeHorizontal = new Vector3(0.12f, 0.12f, 0);
        [SerializeField] private Vector3 _destroyRangeVertical = new Vector3(0.12f, 0.12f, 0);
        [SerializeField] private HandPosScript _hand;
        [SerializeField] private float _damage = 5;
        private Vector3 _currentDestroyRange = new Vector3(0.12f, 0.12f, 0);

        void FixedUpdate()
        {
            if (_timeBtwDestroy <= 0)
            {
                if (Input.GetKey(KeyCode.Space))
                {
                    switch (_hand.HandDirection)
                    {
                        case Direction.Down:
                        {
                            _anim.SetTrigger("DestroyDown");
                            break;
                        }
                        case Direction.Up:
                        {
                            _anim.SetTrigger("DestroyUp");
                            break;
                        }
                        default:
                        {
                            _anim.SetTrigger("DestroyHorizontal");
                            break;
                        }
                    }
                    
                    OnDamage();
                    OnDestroy();
                    _timeBtwDestroy = _destroyDelay;
                }
            }
            else _timeBtwDestroy -= Time.deltaTime;
        }

        void OnDamage()
        {
            _currentDestroyRange =
                _hand.HandDirection == Direction.Up ? _destroyRangeVertical : _destroyRangeHorizontal;

            Collider2D[] enemies =
                Physics2D.OverlapBoxAll(_hand.transform.position, _currentDestroyRange, 0f, _enemy);

            foreach (var e in enemies)
                e.GetComponent<Enemy>().TakeDamage(_damage);
        }

        void OnDestroy()
        {
            _currentDestroyRange =
                _hand.HandDirection == Direction.Up ? _destroyRangeVertical : _destroyRangeHorizontal;

            Collider2D[] objectsToDestroy =
                Physics2D.OverlapBoxAll(_hand.transform.position, _currentDestroyRange, 0f, _destroyableEnviroment);
            if (objectsToDestroy.Length > 0)
            {
                var minDistanceBtwPlayer = float.MaxValue;
                var currentObjWithMinDistance = objectsToDestroy[0];
                foreach (var obj in objectsToDestroy)
                {
                    var distance = (transform.position - obj.GetComponent<Transform>().position).magnitude;
                    if (distance < minDistanceBtwPlayer)
                    {
                        minDistanceBtwPlayer = distance;
                        currentObjWithMinDistance = obj;
                    }
                }

                currentObjWithMinDistance.GetComponent<EnviromentDestroyable>().ToDestroy();
            }
        }

        void OnDrawGizmosSelected()
        {
            Gizmos.DrawWireCube(_hand.transform.position, new Vector3(0.12f, 0.12f, 0));
        }
    }
}