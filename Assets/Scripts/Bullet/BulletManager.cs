using UnityEngine;

namespace Bullet
{
    public class BulletManager : MonoBehaviour
    {
        [SerializeField] private GameObject _mockBullet;
        [SerializeField] private Transform _bulletPrefab;
        [SerializeField] private float _mockBulletBaseScale = 0.1f;
        [SerializeField] private float _mockBulletScalePerCharge = 1f;
        [SerializeField] private float _bulletSpeed = 15f;
        [SerializeField] private float _bulletLifetime = 5f;
        [SerializeField] private float _baseExplosionRadius = 1f;
        [SerializeField] private float _explosionRadiusPerCharge = 2f;

        private float _charge;

        public float Charge
        {
            get => _charge;
            set
            {
                _charge = Mathf.Max(0f, value);
                UpdateCharge(_charge);
            }
        }

        private void Awake()
        {
            _mockBullet.SetActive(false);
        }

        public void BeginCharge()
        {
            _charge = 0f;

            if (_mockBullet != null)
            {
                _mockBullet.SetActive(true);
                _mockBullet.transform.localScale = Vector3.one * _mockBulletBaseScale;
            }
        }

        public void UpdateCharge(float charge)
        {
            float scale = _mockBulletBaseScale + charge * _mockBulletScalePerCharge;
            _mockBullet.transform.localScale = Vector3.one * scale;
        }

        public void FireBullet(Vector3 direction, float chargeAmount)
        {
            Vector3 spawnPosition = _mockBullet.transform.position;

            if (_mockBullet != null)
            {
                _mockBullet.SetActive(false);
            }

            if (_bulletPrefab == null)
            {
                _charge = 0f;
                return;
            }

            Transform bulletInstance = Instantiate(_bulletPrefab, spawnPosition, Quaternion.identity);

            float bulletScale = _mockBulletBaseScale + chargeAmount * _mockBulletScalePerCharge;
            bulletInstance.localScale = Vector3.one * bulletScale;

            float explosionRadius = _baseExplosionRadius + chargeAmount * _explosionRadiusPerCharge;

            if (bulletInstance.TryGetComponent(out Bullet bullet))
            {
                bullet.Initialize(direction, _bulletSpeed, explosionRadius, _bulletLifetime);
            }

            _charge = 0f;
        }

        public Vector3 GetSpawnPosition()
        {
            return _mockBullet.transform.position;
        }
    }
}