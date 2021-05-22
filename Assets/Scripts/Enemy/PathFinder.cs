using System;
using System.Collections.Generic;
using System.Linq;
using General;
using UnityEditorInternal;
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

    public class PathFinder
    {
        private const float Turn = 0.16f;
        private LayerMask _solidLayer;

        private Transform _playerTransform;

        public PathFinder(LayerMask solidLayer, Transform playerTransform)
        {
            _solidLayer = solidLayer;
            _playerTransform = playerTransform;
        }

        public static Vector3 RoundVector(Vector3 vector)
        {
            return new Vector3((float) Math.Round(vector.x / 0.16f) * 0.16f + 0.08f,
                (float) Math.Round(vector.y / 0.16f) * 0.16f + 0.08f);
        }

        private readonly List<Vector3> _possibleMoves = new List<Vector3>
        {
            new Vector3(-Turn, 0),
            new Vector3(Turn, 0),
            new Vector3(0, -Turn),
            new Vector3(0, Turn),
        };

        public List<Vector3> FindShortestPath(Vector3 initialPosition)
        {
            var target = GetNearestTarget(initialPosition);
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

            return default;
        }

        public Vector3 GetNearestTarget(Vector3 initialPosition)
        {
            var nearest = _playerTransform.position;

            foreach (var target in Map.PlayerSideTransforms)
            {
                if ((target - initialPosition).magnitude <
                    (nearest - initialPosition).magnitude)
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

            return result;
        }
    }
}
