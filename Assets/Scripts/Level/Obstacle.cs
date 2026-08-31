using System;
using UnityEngine;

namespace Level
{
    public class Obstacle : MonoBehaviour
    {
        public event Action<Obstacle> ObstacleDestroyed;
        public void Explode()
        {
            ObstacleDestroyed?.Invoke(this);
            Destroy(gameObject);
        }
    }
}