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
        [SerializeField] private LayerMask _objectToDestroy;
        [SerializeField] private Animator _anim;
        [SerializeField] private Vector3 _destroyRangeHorizontal = new Vector3(0.12f, 0.12f, 0);
        [SerializeField] private Vector3 _destroyRangeVertical = new Vector3(0.12f, 0.12f, 0);
        [SerializeField] private HandPosScript _hand;
        private Vector3 _currentDestroyRange = new Vector3(0.12f, 0.12f, 0);

        void FixedUpdate()
        {
            if (_timeBtwDestroy <= 0)
            {
                if (Input.GetKey(KeyCode.Space))
                {
                    if (_hand.HandDirection == Direction.Left || _hand.HandDirection == Direction.Right)
                        _anim.SetTrigger("DestroyHorizontal");
                    else if (_hand.HandDirection == Direction.Up)
                        _anim.SetTrigger("DestroyUp");
                    else _anim.SetTrigger("DestroyDown");
                    OnDestroy();
                    _timeBtwDestroy = _destroyDelay;
                }
            }
            else _timeBtwDestroy -= Time.deltaTime;
        }

        void OnDestroy()
        {
            _currentDestroyRange =
                _hand.HandDirection == Direction.Up ? _destroyRangeVertical : _destroyRangeHorizontal;

            Collider2D[] objectsToDestroy =
                Physics2D.OverlapBoxAll(_hand.transform.position, _currentDestroyRange, 0f, _objectToDestroy);
            if (objectsToDestroy.Length > 0)
                objectsToDestroy[0].GetComponent<EnviromentDestroyable>().ToDestroy();

            /*foreach (var obj in objectsToDestroy)
                obj.GetComponent<EnviromentDestroyable>().ToDestroy();*/
        }

        void OnDrawGizmosSelected()
        {
            Gizmos.DrawWireCube(_hand.transform.position, new Vector3(0.12f, 0.12f, 0));
        }
    }
}