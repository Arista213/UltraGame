using System.Collections;
using System.Collections.Generic;
using System.Threading;
using UnityEngine;

namespace Assets.Scripts
{
    public class EnviromentDestroyable : MonoBehaviour
    {
        //[SerializeField] private GameObject destroyEffect;
        public void ToDestroy()
        {
            //Instantiate(destroyEffect);
            Destroy(gameObject);
        }
    }
}