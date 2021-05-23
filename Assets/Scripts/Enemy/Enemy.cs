using System;
using System.Collections.Generic;
using System.Linq;
using General;
using UnityEngine;

namespace Enemy
{
    public class Enemy : Damageable
    {
        [SerializeField] private float _damage;
        [SerializeField] private float _maxSpeed;
        [SerializeField] private float _attackRange;
        [SerializeField] private float _attackDelay;

        [SerializeField] private LayerMask _playerSideMask;

        [SerializeField] private Animator _anim;
        [SerializeField] private AudioSource _audioSource;
        [SerializeField] private AudioClip _damageSound;
        [SerializeField] private Transform _enemyTransform;
        [NonSerialized] public static bool PlayerStatus = true;
        
        private Rigidbody2D _rigidbody2D;
        private List<Vector3> _moveList = new List<Vector3>();
        private bool _isFacingRight = true;
        private float _attackCooldown;
        private bool _alive = true;

        private void Start()
        {
            _rigidbody2D = GetComponent<Rigidbody2D>();
            InvokeRepeating(nameof(UpdateMoveList), 0f, 0.8f);
        }

        private void FixedUpdate()
        {
            if (_alive)
            {
                CheckDamageStatus();
                if (PlayerStatus)
                    SeekTarget();
                else
                {
                    CancelInvoke(nameof(UpdateMoveList));
                }
            }
        }

        public override void TakeDamage(float damage)
        {
            Health -= damage;
            healthBar.fillAmount = Health / MaxHealth;
            if (Health <= 0)
            {
                _rigidbody2D.velocity = default;
                _anim.SetTrigger("Dead");
                _alive = false;
                Destroy(gameObject, 0.5f);
            }
        }

        private void UpdateMoveList()
        {
            _moveList = Map.PathFinder.FindShortestPath(transform.position);
            DrawPath(_moveList, transform.position);
        }

        private void CheckDamageStatus()
        {
            if (_attackCooldown <= 0)
            {
                TryToDamage();
                _attackCooldown = _attackDelay;
            }
            else _attackCooldown -= Time.deltaTime;
        }

        private void TryToDamage()
        {
            Collider2D target =
                Physics2D.OverlapCircle(transform.position, _attackRange, _playerSideMask);
            if (target != null)
            {
                _audioSource.PlayOneShot(_damageSound);
                _anim.SetTrigger("Attack");
                target.GetComponent<Damageable>().TakeDamage(_damage);
            }
        }

        private void Move(Vector3 nextMove)
        {
            _anim.SetFloat("Moving", _rigidbody2D.velocity.magnitude);
            var position = transform.position;
            _rigidbody2D.velocity = (nextMove - position).normalized * _maxSpeed;
            var dirX = nextMove.x - position.x;

            if (dirX > 0 && !_isFacingRight)
                Flip();
            else if (dirX < 0 && _isFacingRight)
                Flip();
        }


        private void SeekTarget()
        {
            if (_moveList == null)
            {
                return;
            }

            var target = Map.PathFinder.GetNearestTarget(transform.position);
            if ((transform.position - target).magnitude <= 0.08f)
                _rigidbody2D.velocity = default;
            else
            {
                var nextMove = _moveList.FirstOrDefault();
                if (nextMove != default)
                {
                    if ((nextMove - transform.position).magnitude <= 0.16f) _moveList.RemoveAt(0);
                    Move(nextMove);
                }
                else
                    _rigidbody2D.velocity = default;
            }
        }

        private void Flip()
        {
            _isFacingRight = !_isFacingRight;
            Vector3 theScale = _enemyTransform.localScale;
            theScale.x *= -1;
            _enemyTransform.localScale = theScale;
        }

        private void OnDrawGizmosSelected()
        {
            Gizmos.DrawWireSphere(transform.position, _attackRange);
        }


        private void DrawPath(List<Vector3> path, Vector3 initialPosition)
        {
            if (path != null && path.Any())
            {
                var prev = initialPosition;
                foreach (var e in path)
                {
                    DrawLine(prev, e, Color.green);
                    prev = e;
                }
            }
        }


        private void DrawLine(Vector3 start, Vector3 end, Color color, float duration = 1f)
        {
            GameObject myLine = new GameObject();
            myLine.transform.position = start;
            myLine.AddComponent<LineRenderer>();
            LineRenderer lr = myLine.GetComponent<LineRenderer>();
            lr.material = new Material(Shader.Find("Sprites/Default"));
            lr.startColor = color;
            lr.endColor = color;
            lr.startWidth = 0.004f;
            lr.endWidth = 0.004f;
            lr.SetPosition(0, start);
            lr.SetPosition(1, end);
            Destroy(myLine, duration);
        }
    }
}