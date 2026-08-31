using Level;
using UnityEngine;

namespace Player
{
    public class PlayerMovementController : MonoBehaviour
    {
        [SerializeField] private Transform _playerBody;
        [SerializeField] private PathClearanceChecker _clearanceChecker;
        [SerializeField] private PlayerChargeHandler _chargeHandler;
        [SerializeField] private Transform _finalPosition;
        [SerializeField] private float _moveSpeed = 3f;

        private bool _isMoving;

        private void Awake()
        {
            _clearanceChecker.PathClear += StartMoving;
        }

        private void OnDestroy()
        {
            _clearanceChecker.PathClear -= StartMoving;
        }

        private void Update()
        {
            HandleMoving();
        }

        private void StartMoving()
        {
            _isMoving = true;
        }

        private void HandleMoving()
        {
            if (!_isMoving || _chargeHandler.IsCharging)
            {
                return;
            }

            _playerBody.position = Vector3.MoveTowards(_playerBody.position, _finalPosition.position, _moveSpeed * Time.deltaTime);

            if (_playerBody.position == _finalPosition.position)
            {
                _isMoving = false;
            }
        }
    }
}