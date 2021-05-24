using System;
using System.Collections.Generic;
using System.Linq;
using General;
using UnityEditorInternal;
using UnityEngine;

namespace Enemy
{
    public class PathFinder
    {
        private const float CellSize = 0.16f;
        private LayerMask _solidLayer;

        private Transform _playerTransform;

        public PathFinder(LayerMask solidLayer, Transform playerTransform)
        {
            _solidLayer = solidLayer;
            _playerTransform = playerTransform;
        }

        public static Vector3 RoundVector(Vector3 vector)
        {
            var x = vector.x < 0
                ? vector.x + (CellSize / 2 - vector.x % CellSize) - CellSize
                : vector.x + (CellSize / 2 - vector.x % CellSize);
            var y = vector.y < 0
                ? vector.y + (CellSize / 2 - vector.y % CellSize) - CellSize
                : vector.y + (CellSize / 2 - vector.y % CellSize);
            return new Vector3(x, y);
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
            if ((target - initialPosition).magnitude <= CellSize)
                return new List<Vector3> {target};
            
            var start = RoundVector(initialPosition);
            var end = RoundVector(target);
            var a = new Vector3(CellSize / 2, CellSize / 2);
            var visitedPoints = new HashSet<Vector3> {start};
            var queue = new Queue<SinglyLinkedList<Vector3>>();
            queue.Enqueue(new SinglyLinkedList<Vector3>(start, null));

            while (queue.Count > 0)
            {
                var currentPoint = queue.Dequeue();

                var p = _possibleMoves.Select(nextMove => currentPoint.Value + nextMove);
                var d = p.Where(nextPoint => !Physics2D.OverlapCircle(nextPoint, 0.03f, _solidLayer));
                var v = d.Where(nextPoint => !visitedPoints.Contains(nextPoint)).ToList();

                foreach (var nextPoint in v)
                {
                    if (queue.Count > 2000)
                        return GetMoveList(queue.Last(), target);

                    var tempSinglyLinkedList = new SinglyLinkedList<Vector3>(nextPoint, currentPoint);
                    queue.Enqueue(tempSinglyLinkedList);


                    if (Physics2D.OverlapCircle(nextPoint, CellSize / 2, Map.PlayerSideLayer))
                    {
                        return GetMoveList(queue.Last(), target);
                    }

                    visitedPoints.Add(nextPoint);
                }
            }

            return new List<Vector3> {start};
        }

        public Vector3 GetNearestTarget(Vector3 initialPosition)
        {
            return Map.PlayerSideTransforms.Concat(new[] {_playerTransform.position})
                .OrderBy(x => (initialPosition - x).magnitude).FirstOrDefault();
        }

        public static List<Vector3> GetMoveList(SinglyLinkedList<Vector3> path, Vector3 target)
        {
            if (path == null) return null;
            var result = new List<Vector3>();
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

            return result.Skip(1).ToList();
        }
    }
}