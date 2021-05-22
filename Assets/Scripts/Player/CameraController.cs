using UnityEngine;

namespace Player
{
    public class CameraController : MonoBehaviour
    {
        public float dumping = 2.5f;
        private float offsetX;
        private float offsetY;
        private Transform player;
        private Vector3 offset = new Vector3(0.5f, 0.5f);

        void Start()
        {
            FindPlayer();
        }

        public void FindPlayer()
        {
            player = GameObject.FindGameObjectWithTag("Player").transform;
            transform.Translate(new Vector3(player.position.x, player.position.y, transform.position.z))    ;
            // transform.position = new Vector3(player.position.x, player.position.y,
            //     transform.position.z);
        }

        void Update()
        {
            offsetX = Input.GetAxis("Horizontal");
            offsetY = Input.GetAxis("Vertical");

            if (player)
            {
                Vector3 target = new Vector3(player.position.x + offset.x * offsetX,
                    player.position.y + offset.y * offsetY, transform.position.z);
                Vector3 currentPosition = Vector3.Lerp(transform.position, target, dumping * Time.deltaTime * 5);
                transform.position = currentPosition;
            }
        }
    }
}