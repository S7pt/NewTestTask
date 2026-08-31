using Level;
using System.Collections.Generic;
using UnityEngine;

namespace Bullet
{
    public class Bullet : MonoBehaviour
    {
        private Vector3 _direction;
        private float _speed;
        private float _explosionRadius;
        private float _maxLifetime;
        private Rigidbody _rigidbody;
        private float _lifetime;

        private void Awake()
        {
            _rigidbody = GetComponent<Rigidbody>();
            _rigidbody.useGravity = false;
        }

        public void Initialize(Vector3 direction, float speed, float explosionRadius, float lifetime)
        {
            _direction = direction.sqrMagnitude > 0.0001f ? direction.normalized : transform.forward;
            _speed = speed;
            _explosionRadius = explosionRadius;
            _maxLifetime = lifetime;
        }

        private void FixedUpdate()
        {
            HandleMovement();
        }

        private void Update()
        {
            HandleLifetime();
        }

        private void OnTriggerEnter(Collider other)
        {
            TryExplode(other);
        }

        private void HandleMovement()
        {
            _rigidbody.MovePosition(_rigidbody.position + _direction * _speed * Time.fixedDeltaTime);
        }

        private void TryExplode(Collider other)
        {
            if (other.TryGetComponent(out Obstacle obstacle))
            {
                Explode(obstacle);
            }
        }

        private void HandleLifetime()
        {
            _lifetime += Time.deltaTime;
            if (_lifetime >= _maxLifetime)
            {
                Destroy(gameObject);
            }
        }

        private void Explode(Obstacle obstacle)
        {
            Queue<Vector3> pending = new Queue<Vector3>();
            Collider[] hits = Physics.OverlapSphere(obstacle.transform.position, _explosionRadius);
            obstacle.Explode();
            foreach (Collider potentialObstacle in hits)
            {
                if (potentialObstacle.TryGetComponent(out Obstacle neighbouringObstacle))
                {
                    neighbouringObstacle.Explode();
                }
            }

            Destroy(gameObject);
        }
    }
}