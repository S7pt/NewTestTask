using Bullet;
using System;
using UnityEngine;

namespace Player
{
    public class PlayerChargeHandler : MonoBehaviour
    {
        [SerializeField] private Transform _playerBody;
        [SerializeField] private BulletManager _bulletManager;
        [SerializeField] private float _chargeGrowthRate = 0.5f;
        [SerializeField] private float _minCriticalScale = 0.2f;

        public event Action PlayerDepleted;
        public event Action<float> ShotFired;
        public event Action<float> SizeChanged;

        private bool _isCharging;
        private bool _canCharge = true;
        private float _chargeStartTime;
        private float _playerScaleAtChargeStart;

        public bool IsCharging { get => _isCharging; set => _isCharging = value; }
        public bool CanCharge { get => _canCharge; set => _canCharge = value; }

        private void Update()
        {
            HandleCharging();
        }

        private void HandleCharging()
        {
            if (!IsCharging || !CanCharge)
            {
                return;
            }

            float elapsed = Time.time - _chargeStartTime;
            float charge = elapsed * _chargeGrowthRate;
            float remainingScale = _playerScaleAtChargeStart - charge;

            if (remainingScale <= _minCriticalScale)
            {
                charge = _playerScaleAtChargeStart - _minCriticalScale;
                ApplyPlayerScale(_minCriticalScale);
                _bulletManager.Charge = charge;
                IsCharging = false;
                PlayerDepleted?.Invoke();
                return;
            }

            ApplyPlayerScale(remainingScale);
            _bulletManager.Charge = charge;
        }

        public void StartCharging()
        {
            if (IsCharging || !CanCharge)
            {
                return;
            }

            IsCharging = true;
            _chargeStartTime = Time.time;
            _playerScaleAtChargeStart = _playerBody.localScale.x;
            _bulletManager.BeginCharge();
        }

        public void StopCharging(Vector3 position)
        {
            if (!IsCharging)
            {
                return;
            }

            IsCharging = false;

            float elapsed = Time.time - _chargeStartTime;
            float maxAvailableCharge = Mathf.Max(0f, _playerScaleAtChargeStart - _minCriticalScale);
            float chargeAmount = Mathf.Clamp(elapsed * _chargeGrowthRate, 0f, maxAvailableCharge);

            Vector3 direction = GetDirectionToTarget(position);
            _bulletManager.FireBullet(direction, chargeAmount);

            ShotFired?.Invoke(chargeAmount);
        }

        private Vector3 GetDirectionToTarget(Vector3 position)
        {
            Vector3 direction = position - _bulletManager.GetSpawnPosition();
            direction.y = 0f;

            return direction.normalized;
        }

        private void ApplyPlayerScale(float scale)
        {
            _playerBody.localScale = Vector3.one * scale;
            SizeChanged?.Invoke(scale);
        }

    }
}