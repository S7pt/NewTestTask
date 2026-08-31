using Player;
using UnityEngine;

namespace Level
{
    public class DoorOpener : MonoBehaviour
    {
        [SerializeField] private Animator _castleAnimator;
        private const string OPEN_TRIGGER = "Open";
        private readonly int OPEN_HASH = Animator.StringToHash(OPEN_TRIGGER);

        private void OnTriggerEnter(Collider other)
        {
            if (other.GetComponent<PlayerMovementController>() != null)
            {
                _castleAnimator.SetTrigger(OPEN_HASH);
            }
        }
    }
}