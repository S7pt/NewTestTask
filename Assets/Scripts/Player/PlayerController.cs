using Level;
using System;
using UnityEngine;
using Utility;

namespace Player
{
    public class PlayerController : MonoBehaviour
    {
        [SerializeField] private InputHandler _input;
        [SerializeField] private PlayerChargeHandler _chargeHandler;
        [SerializeField] private PathClearanceChecker _clearanceChecker;

        private void Awake()
        {
            SubscribeToInput();
            _clearanceChecker.PathClear += DisableCharging;
        }

        private void OnDestroy()
        {
            UnsubscribeFromInput();
            _clearanceChecker.PathClear -= DisableCharging;
        }

        private void SubscribeToInput()
        {
            _input.FingerPressed += OnFingerPressed;
            _input.FingerReleased += OnFingerReleased;
        }

        private void UnsubscribeFromInput()
        {
            _input.FingerPressed -= OnFingerPressed;
            _input.FingerReleased -= OnFingerReleased;
        }

        private void DisableCharging()
        {
            _chargeHandler.CanCharge = false;
        }

        private void OnFingerPressed(Vector3 position)
        {
            _chargeHandler.StartCharging();
        }

        private void OnFingerReleased(Vector3 position)
        {
            _chargeHandler.StopCharging(position);
        }

    }
}