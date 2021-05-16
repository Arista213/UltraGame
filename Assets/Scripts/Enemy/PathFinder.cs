using System;
using System.Collections.Generic;
using System.Linq;
using General;
using UnityEngine;

namespace Enemy
{
    public class SinglyLinkedList<T>
    {
        public SinglyLinkedList(T value, SinglyLinkedList<T> previous)
        {
            Value = value;
            Previous = previous;
            Length += 1 + previous?.Length ?? 1;
        }

        public SinglyLinkedList<T> Previous { get; }
        public T Value { get; set; }
        public int Length { get; set; }
    }

    public class PathFinder : MonoBehaviour
    {
        private const float Turn = 0.16f;
        private LineRenderer _lr;

        [SerializeField] private LayerMask _solidLayer;
        private Rigidbody2D _rigidbody2D;

        private Transform _playerTransform;
        private List<Vector3> _moveList = new List<Vector3>();

        public static Vector3 RoundVector(Vector3 vector)
        {
            return new Vector3((float) Math.Round(vector.x / 0.16f) * 0.16f + 0.08f,
                (float) Math.Round(vector.y / 0.16f) * 0.16f + 0.08f);
        }

        private void Start()
        {
            _playerTransform = GameObject.FindWithTag("Player").transform;
            _rigidbody2D = GetComponent<Rigidbody2D>();
            _moveList.Add(transform.position);
            InvokeRepeating(nameof(UpdateMove), 0f, 0.8f);
        }

        private void FixedUpdate()
        {
            SeekTarget();
        }

        private readonly List<Vector3> _possibleMoves = new List<Vector3>
        {
            new Vector3(-Turn, 0),
            new Vector3(Turn, 0),
            new Vector3(0, -Turn),
            new Vector3(0, Turn),
        };

        // private void InitPossibleMoves()
        // {
        //     var turn = 0.16f;
        //     for (float i = -turn; i <= turn; i += turn)
        //     for (float j = -turn; j <= turn; j += turn)
        //         if (!(i == 0 && j == 0))
        //             _possibleMoves.Add(new Vector3(i, j));
        // }


        public List<Vector3> FindShortestPath(Vector3 initialPosition)
        {
            var target = GetNearestTarget();
            var start = RoundVector(initialPosition);
            var end = RoundVector(target);
            var visitedPoints = new HashSet<Vector3> {start};
            var queue = new Queue<SinglyLinkedList<Vector3>>();
            queue.Enqueue(new SinglyLinkedList<Vector3>(start, null));

            while (queue.Count > 0)
            {
                var currentPoint = queue.Dequeue();
                if ((currentPoint.Value - end).magnitude <= 0.1f) return new List<Vector3> {end};

                var p = _possibleMoves.Select(nextMove => currentPoint.Value + nextMove)
                    .Where(nextPoint => (nextPoint - end).magnitude <= 0.16f ||
                                        !Physics2D.OverlapCircle(nextPoint, 0.01f, _solidLayer)
                                        && !visitedPoints.Contains(nextPoint)).ToList();

                foreach (var nextPoint in p)
                {
                    if (queue.Count > 400)
                        return default;

                    var tempSinglyLinkedList = new SinglyLinkedList<Vector3>(nextPoint, currentPoint);
                    queue.Enqueue(tempSinglyLinkedList);

                    if ((end - nextPoint).magnitude <= 0.08f)
                        return GetMoveList(queue.Last(), target);

                    visitedPoints.Add(nextPoint);
                }
            }

            print("TargetNotFound");
            return default;
        }

        private Vector3 GetNearestTarget()
        {
            var nearest = _playerTransform.position;
            foreach (var target in Map.PlayerSideTransforms)
            {
                if ((target - transform.position).magnitude <
                    (nearest - transform.position).magnitude)
                    nearest = target;
            }

            return nearest;
        }

        private List<Vector3> GetMoveList(SinglyLinkedList<Vector3> path, Vector3 target)
        {
            if (path == null) return null;
            var result = new List<Vector3>();
            result.Add(path.Value);
            if (path.Previous != null)
            {
                var previous = path.Previous;
                while (previous != null)
                {
                    result.Add(previous.Value);
                    previous = previous.Previous;
                }

                result.Reverse();
                result.Add(target);
            }

            //DrawPath(result);
            return result;
        }

        private void UpdateMove()
        {
            _moveList = FindShortestPath(transform.position);
        }

        private void DrawPath(List<Vector3> path)
        {
            if (path != null && path.Any())
            {
                var prev = transform.position;
                foreach (var e in path)
                {
                    DrawLine(prev, e, Color.green);
                    prev = e;
                }
            }
        }

        private void SeekTarget()
        {
            if (_moveList == null)
            {
                return;
            }

            var target = GetNearestTarget();
            if ((transform.position - target).magnitude <= 0.08f)
                _rigidbody2D.velocity = default;
            else
            {
                var nextMove = _moveList.FirstOrDefault();
                if (nextMove != default)
                {
                    if ((nextMove - transform.position).magnitude <= 0.16f) _moveList.RemoveAt(0);
                    var direction = (nextMove - transform.position).normalized;
                    _rigidbody2D.velocity = new Vector2(direction.x, direction.y) * 0.5f;
                }
                else
                    _rigidbody2D.velocity = default;
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