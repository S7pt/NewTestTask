using System;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

namespace Level
{
    public class PathClearanceChecker : MonoBehaviour
    {
        private HashSet<Obstacle> _obstaclesOnPath;

        public event Action PathClear;

        private void Awake()
        {
            _obstaclesOnPath = new HashSet<Obstacle>();
        }

        private void OnTriggerEnter(Collider other)
        {
            if (other.TryGetComponent(out Obstacle obstacle))
            {
                _obstaclesOnPath.Add(obstacle);
                obstacle.ObstacleDestroyed += OnObstacleDestroyed;
            }
        }

        private void OnObstacleDestroyed(Obstacle destroyedObstacle)
        {
            destroyedObstacle.ObstacleDestroyed -= OnObstacleDestroyed;
            _obstaclesOnPath.Remove(destroyedObstacle);
            if (_obstaclesOnPath.Count == 0)
            {
                PathClear?.Invoke();
            }
        }

        private void OnTriggerExit(Collider other)
        {
            if (!other.TryGetComponent(out Obstacle obstacle))
            {
                return;
            }
            _obstaclesOnPath.Remove(obstacle);
            if (_obstaclesOnPath.Count == 0)
            {
                PathClear?.Invoke();
            }
        }
    }
}