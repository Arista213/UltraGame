using UnityEngine;

namespace General
{
    public class Destroyable : MonoBehaviour
    {
        //[SerializeField] private GameObject destroyEffect;
        public void ToDestroy()
        {
            //Instantiate(destroyEffect);
            Destroy(gameObject);
        }
    }
}