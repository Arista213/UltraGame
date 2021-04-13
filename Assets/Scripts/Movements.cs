using UnityEngine;
using UnityEngine.Animations;

namespace Assets.Scripts
{
    public class Movements : MonoBehaviour
    {
        [SerializeField] private float _mSpeed = 5f;
        private bool isFacingRight = true;

        private void FixedUpdate()
        {
            float moveX = Input.GetAxis("Horizontal");
            float moveY = Input.GetAxis("Vertical");
            GetComponent<Rigidbody2D>().velocity = new Vector2(_mSpeed * moveX, _mSpeed * moveY);

            if (moveX > 0 && !isFacingRight)
                Flip();
            else if (moveX < 0 && isFacingRight)
                Flip();
        }

        private void Flip()
        {
            isFacingRight = !isFacingRight;
            Vector3 theScale = transform.localScale;
            theScale.x *= -1;
            transform.localScale = theScale;
        }
    }
}