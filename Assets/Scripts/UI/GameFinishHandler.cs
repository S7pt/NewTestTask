using Level;
using Player;
using System;
using UnityEngine;
using Utility;

namespace UI
{
    public class GameFinishHandler : MonoBehaviour
    {
        [SerializeField] private GameFinishScreen _winScreen;
        [SerializeField] private GameFinishScreen _loseScreen;
        [SerializeField] private PlayerChargeHandler _playerChargeHandler;
        [SerializeField] private InputHandler _inputHandler;
        [SerializeField] private FinishChecker _finishChecker;

        private void Awake()
        {
            UnfreezeGame();
            _playerChargeHandler.PlayerDepleted += TriggerLoss;
            _finishChecker.PlayerReachedFinish += TriggerWin;
        }

        private void OnDestroy()
        {
            _playerChargeHandler.PlayerDepleted -= TriggerLoss;
            _finishChecker.PlayerReachedFinish -= TriggerWin;
        }

        private void TriggerLoss()
        {
            FreezeGame();
            _loseScreen.Show();
        }

        private void TriggerWin()
        {
            FreezeGame();
            _winScreen.Show();
        }

        private void FreezeGame()
        {
            Time.timeScale = 0;
            _inputHandler.StopCapturingInput();
        }

        private void UnfreezeGame()
        {
            Time.timeScale = 1;
            _inputHandler.StartCapturingInput();
        }
    }
}