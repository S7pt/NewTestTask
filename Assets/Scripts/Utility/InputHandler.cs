using System;
using UnityEngine;
using UnityEngine.InputSystem.EnhancedTouch;
using Touch = UnityEngine.InputSystem.EnhancedTouch.Touch;

namespace Utility
{
    public class InputHandler : MonoBehaviour
    {
        [SerializeField] private LayerMask _groundLayer;
        public event Action<Vector3> FingerPressed;
        public event Action<Vector3> FingerReleased;

        private void Awake()
        {
            StartCapturingInput();
        }

        private void OnDestroy()
        {
            StopCapturingInput();
        }

        public void StopCapturingInput()
        {
            Touch.onFingerDown -= OnFingerDown;
            Touch.onFingerUp -= OnFingerUp;
        }

        public void StartCapturingInput()
        {
            EnhancedTouchSupport.Enable();
            Touch.onFingerDown += OnFingerDown;
            Touch.onFingerUp += OnFingerUp;
        }

        private void OnFingerDown(Finger finger)
        {
            Vector3? point = GetTouchWorldPoint(finger.screenPosition);

            if (point.HasValue)
            {
                FingerPressed?.Invoke(point.Value);
            }
        }

        private void OnFingerUp(Finger finger)
        {
            Vector3? point = GetTouchWorldPoint(finger.screenPosition);

            if (point.HasValue)
            {
                FingerReleased?.Invoke(point.Value);
            }
        }

        private Vector3? GetTouchWorldPoint(Vector2 screenPosition)
        {
            Ray ray = Camera.main.ScreenPointToRay(screenPosition);

            if (Physics.Raycast(ray, out RaycastHit hit, Mathf.Infinity, _groundLayer))
            {
                return hit.point;
            }

            return null;
        }
    }
}