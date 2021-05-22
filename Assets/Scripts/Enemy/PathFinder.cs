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
        private const float CellSize = 0.15f;
        private LayerMask _solidLayer;

        private Transform _playerTransform;

        public PathFinder(LayerMask solidLayer, Transform playerTransform)
        {
            _solidLayer = solidLayer;
            _playerTransform = playerTransform;
        }

        public static Vector3 RoundVector(Vector3 vector)
        {
            return new Vector3((float) Math.Round(vector.x / CellSize) * CellSize + CellSize / 2,
                (float) Math.Round(vector.y / CellSize) * CellSize + CellSize / 2);
        }

        private readonly List<Vector3> _possibleMoves = new List<Vector3>
        {
            new Vector3(-CellSize, 0),
            new Vector3(CellSize, 0),
            new Vector3(0, -CellSize),
            new Vector3(0, CellSize),
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
                    .Where(nextPoint => (nextPoint - end).magnitude <= CellSize ||
                                        !Physics2D.OverlapCircle(nextPoint, 0.01f, _solidLayer)
                                        && !visitedPoints.Contains(nextPoint)).ToList();

                foreach (var nextPoint in p)
                {
                    if (queue.Count > 2000)
                        return default;

                    var tempSinglyLinkedList = new SinglyLinkedList<Vector3>(nextPoint, currentPoint);
                    queue.Enqueue(tempSinglyLinkedList);

                    if ((end - nextPoint).magnitude <= CellSize / 2)
                        return GetMoveList(queue.Last(), target);

                    visitedPoints.Add(nextPoint);
                }
            }

            return default;
        }

        public Vector3 GetNearestTarget(Vector3 initialPosition)
        {
            if (_playerTransform != null)
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

            return default;
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