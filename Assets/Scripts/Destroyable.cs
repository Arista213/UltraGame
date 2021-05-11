using System.Collections;
using System.Collections.Generic;
using System.Threading;
using UnityEngine;

public class Destroyable : MonoBehaviour
{
    //[SerializeField] private GameObject destroyEffect;
    public void ToDestroy()
    {
        //Instantiate(destroyEffect);
        Destroy(gameObject);
    }
}