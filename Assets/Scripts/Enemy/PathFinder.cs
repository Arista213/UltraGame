using System;
using System.Collections.Generic;
using System.Linq;
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

        [SerializeField] private LayerMask _solidLayer;

        //[SerializeField] private Transform _targetTransform;
        private Transform _targetTransform;
        private List<Vector3> _moveList = new List<Vector3>();

        private readonly List<Vector3> _possibleMoves = new List<Vector3>
        {
            new Vector3(-Turn, 0),
            new Vector3(Turn, 0),
            new Vector3(0, -Turn),
            new Vector3(0, Turn),
        };

        void InitPossibleMoves()
        {
            var turn = 0.16f;
            for (float i = -turn; i <= turn; i += turn)
            for (float j = -turn; j <= turn; j += turn)
                if (!(i == 0 && j == 0))
                    _possibleMoves.Add(new Vector3(i, j));
        }

        private void Start()
        {
            _targetTransform = GameObject.FindWithTag("Player").transform;
            _moveList.Add(transform.position);
            InvokeRepeating(nameof(UpdateMove), 0f, 1f);
        }

        public List<Vector3> FindShortestPath(Vector3 start, Vector3 target)
        {
            var visitedPoints = new HashSet<Vector3> {start};
            var queue = new Queue<SinglyLinkedList<Vector3>>();
            queue.Enqueue(new SinglyLinkedList<Vector3>(start, null));
            while (queue.Count > 0)
            {
                var currentPoint = queue.Dequeue();
                if ((currentPoint.Value - target).magnitude <= 0.1f) return new List<Vector3> {target};
                var p = _possibleMoves.Select(nextMove => currentPoint.Value + nextMove)
                    .Where(nextPoint => (nextPoint - target).magnitude <= 0.08f ||
                                        (!Physics2D.OverlapBox(nextPoint, new Vector2(0.02f, 0.02f), _solidLayer)
                                         && !visitedPoints.Contains(nextPoint))).ToList();
                foreach (var nextPoint in p)
                {
                    if (queue.Count > 100)
                    {
                        print("Sheeet");
                        return default;
                    }

                    var tempSinglyLinkedList = new SinglyLinkedList<Vector3>(nextPoint, currentPoint);
                    queue.Enqueue(tempSinglyLinkedList);
                    if ((target - nextPoint).magnitude <= 0.16f)
                    {
                        return GetMoveList(queue.Last());
                    }

                    visitedPoints.Add(nextPoint);
                }
            }

            print("PlayerNotFound");
            return null;
        }

        private List<Vector3> GetMoveList(SinglyLinkedList<Vector3> path)
        {
            if (path == null) return null;
            var result = new List<Vector3>();
            result.Add(path.Value);
            var previous = path.Previous;
            while (previous != null)
            {
                result.Add(previous.Value);
                previous = previous.Previous;
            }

            result.Reverse();
            return result;
        }

        private void UpdateMove()
        {
            _moveList = FindShortestPath(transform.position, _targetTransform.position);
        }

        // private void PrintPath()
        // {
        //     if (_moveList != null)
        //         foreach (var e in _moveList)
        //         {
        //             print(e);
        //         }
        // }

        private void SeekTarget()
        {
            var nextMove = _moveList.FirstOrDefault();
            if (nextMove != default)
            {
                if ((nextMove - transform.position).magnitude <= 0.16f) _moveList.RemoveAt(0);
                var direction = (nextMove - transform.position).normalized;
                GetComponent<Rigidbody2D>().velocity = new Vector2(direction.x, direction.y) * 0.5f;
            }
            else
                GetComponent<Rigidbody2D>().velocity = default;
        }

        private void FixedUpdate()
        {
            SeekTarget();
        }
    }
}