using UnityEngine;

namespace Assets.Scripts
{
    public class Movements : MonoBehaviour
    {
        [SerializeField] private KeyCode KeyW = KeyCode.W;
        [SerializeField] private KeyCode KeyS = KeyCode.S;
        [SerializeField] private KeyCode KeyA = KeyCode.A;
        [SerializeField] private KeyCode KeyD = KeyCode.D;
        [SerializeField] readonly Vector2 _moveY = new (0, 0.1f);
        [SerializeField] readonly Vector2 _moveX = new (0.1f, 0);

        private void FixedUpdate()
        {
            if (Input.GetKey(KeyW))
                GetComponent<Rigidbody2D>().velocity += _moveY;
            if (Input.GetKey(KeyS))
                GetComponent<Rigidbody2D>().velocity -= _moveY;
            if (Input.GetKey(KeyA))
                GetComponent<Rigidbody2D>().velocity -= _moveX;
            if (Input.GetKey(KeyD))
                GetComponent<Rigidbody2D>().velocity += _moveX;
        }
    }
}