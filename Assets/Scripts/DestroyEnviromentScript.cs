using System;
using System.Collections;
using System.Collections.Generic;
using System.Threading;
using UnityEngine;

namespace Assets.Scripts
{
    public class DestroyEnviromentScript : MonoBehaviour
    {
        [SerializeField] private float timeBtwDestroy;
        [SerializeField] private float destroyCooldown;
        [SerializeField] private Transform destroyPos;
        [SerializeField] private LayerMask objectToDestroy;
        [SerializeField] private float destroyRange;
        [SerializeField] private Animator anim;

        void FixedUpdate()
        {
            if (timeBtwDestroy <= 0)
            {
                print(timeBtwDestroy);
                if (Input.GetKey(KeyCode.Space))
                {
                    anim.SetTrigger("Destroy");
                    OnDestroy();
                    timeBtwDestroy = destroyCooldown;
                }
            }
            else timeBtwDestroy -= Time.deltaTime;
        }

        void OnDestroy()
        {
            Collider2D[] objectsToDestroy =
                Physics2D.OverlapCircleAll(destroyPos.position, destroyRange, objectToDestroy);

            print(objectsToDestroy.Length);
            for (int i = 0; i < objectsToDestroy.Length; i++)
            {
                objectsToDestroy[i].GetComponent<EnviromentDestroyable>().ToDestroy();
            }
        }
    }
}