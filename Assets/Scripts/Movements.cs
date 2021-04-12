using UnityEngine;
using UnityEngine.Animations;

namespace Assets.Scripts
{
    public class Movements : MonoBehaviour
    {
        [SerializeField] private float _mSpeed = 5f;

        private void FixedUpdate()
        {
            float moveX = Input.GetAxis("Horizontal");
            float moveY = Input.GetAxis("Vertical");
            GetComponent<Rigidbody2D>().velocity = new Vector2(_mSpeed * moveX, _mSpeed * moveY);
        }
    }
}