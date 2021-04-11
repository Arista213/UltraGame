using UnityEngine;

namespace Assets.Scripts
{
    public class Movements : MonoBehaviour
    {
        [SerializeField] private KeyCode KeyW = KeyCode.W;
        [SerializeField] private KeyCode KeyS = KeyCode.S;
        [SerializeField] private KeyCode KeyA = KeyCode.A;
        [SerializeField] private KeyCode KeyD = KeyCode.D;
        [SerializeField] private Vector2 moveY = new Vector2(0, 0.1f);
        [SerializeField] private Vector2 moveX = new Vector2(0.1f, 0);

        private void FixedUpdate()
        {
            if (Input.GetKey(KeyW))
                GetComponent<Rigidbody2D>().velocity += moveY;
            if (Input.GetKey(KeyS))
                GetComponent<Rigidbody2D>().velocity -= moveY;
            if (Input.GetKey(KeyA))
                GetComponent<Rigidbody2D>().velocity -= moveX;
            if (Input.GetKey(KeyD))
                GetComponent<Rigidbody2D>().velocity += moveX;
        }
    }
}