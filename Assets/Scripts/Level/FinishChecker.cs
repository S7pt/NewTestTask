using Player;
using System;
using UnityEngine;

namespace Level
{
    public class FinishChecker : MonoBehaviour
    {
        public event Action PlayerReachedFinish;

        private void OnTriggerEnter(Collider other)
        {
            if (other.GetComponent<PlayerMovementController>() != null)
            {
                PlayerReachedFinish?.Invoke();
            }
        }
    }
}