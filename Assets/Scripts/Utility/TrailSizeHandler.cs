using Player;
using UnityEngine;

namespace Utility
{
    public class TrailSizeHandler : MonoBehaviour
    {
        [SerializeField] private PlayerChargeHandler _chargeHandler;

        private void Awake()
        {
            _chargeHandler.SizeChanged += OnSizeChanged;
        }

        private void OnDestroy()
        {
            _chargeHandler.SizeChanged -= OnSizeChanged;
        }

        private void OnSizeChanged(float newSize)
        {
            Vector3 newScale = transform.localScale;
            newScale.z = newSize;
            transform.localScale = newScale;
        }
    }
}