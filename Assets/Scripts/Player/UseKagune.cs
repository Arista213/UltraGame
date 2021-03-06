using Assets.Scripts;
using General;
using UnityEngine;

namespace Player
{
    public class UseKagune : MonoBehaviour
    {
        private float _attackCooldown;

        [Header("Attributes")] [SerializeField]
        private float _destroyDelay;

        [SerializeField] private const float DestroyRange = 0.1f;
        [SerializeField] private float _damage = 3;
        private readonly Vector3 _destroyRangeHorizontal = new Vector3(DestroyRange, DestroyRange, 0);
        private readonly Vector3 _destroyRangeVertical = new Vector3(DestroyRange, DestroyRange, 0);
        private Vector3 _currentDestroyRange = new Vector3(DestroyRange, DestroyRange, 0);

        [Header("UnitySetup")] [SerializeField]
        private HandPosScript _hand;

        [SerializeField] private Animator _anim;
        [SerializeField] private LayerMask _destroyableEnviroment;
        [SerializeField] private LayerMask _enemy;
        [SerializeField] private AudioSource _audioSource;
        [SerializeField] private AudioClip _damageSound;


        void FixedUpdate()
        {
            if (_attackCooldown <= 0)
            {
                if (Input.GetKey(KeyCode.Space))
                {
                    switch (_hand.HandDirection)
                    {
                        case Direction.Down:
                        {
                            _anim.SetTrigger("DestroyDown");
                            break;
                        }
                        case Direction.Up:
                        {
                            _anim.SetTrigger("DestroyUp");
                            break;
                        }
                        default:
                        {
                            _anim.SetTrigger("DestroyHorizontal");
                            break;
                        }
                    }

                    _audioSource.PlayOneShot(_damageSound);
                    TryToDamage();
                    TryToDestroy();
                    _attackCooldown = _destroyDelay;
                }
            }
            else _attackCooldown -= Time.deltaTime;
        }

        void TryToDamage()
        {
            Collider2D[] enemies =
                Physics2D.OverlapBoxAll(_hand.transform.position, _currentDestroyRange, 0f, _enemy);

            foreach (var e in enemies)
                e.GetComponent<Enemy.Enemy>().TakeDamage(_damage);
        }

        void TryToDestroy()
        {
            _currentDestroyRange =
                _hand.HandDirection == Direction.Up ? _destroyRangeVertical : _destroyRangeHorizontal;

            Collider2D[] objectsToDestroy =
                Physics2D.OverlapBoxAll(_hand.transform.position, _currentDestroyRange, 0f, _destroyableEnviroment);
            if (objectsToDestroy.Length > 0)
            {
                var minDistanceBtwPlayer = float.MaxValue;
                var currentObjWithMinDistance = objectsToDestroy[0];
                foreach (var obj in objectsToDestroy)
                {
                    var distance = (transform.position - obj.GetComponent<Transform>().position).magnitude;
                    if (distance < minDistanceBtwPlayer)
                    {
                        minDistanceBtwPlayer = distance;
                        currentObjWithMinDistance = obj;
                    }
                }

                currentObjWithMinDistance.GetComponent<Destroyable>().ToDestroy();
            }
        }

        void OnDrawGizmosSelected()
        {
            Gizmos.DrawWireCube(_hand.transform.position, new Vector3(0.12f, 0.12f, 0));
        }
    }
}