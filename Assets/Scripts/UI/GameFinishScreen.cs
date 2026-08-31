using System;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

namespace UI
{
    public class GameFinishScreen : MonoBehaviour
    {
        [SerializeField] private Button _restartButton;
        [SerializeField] private Button _exitButton;

        private void Awake()
        {
            _restartButton.onClick.AddListener(RestartGame);
            _exitButton.onClick.AddListener(ExitGame);
        }

        private void OnDestroy()
        {
            _restartButton.onClick.RemoveListener(RestartGame);
            _exitButton.onClick.RemoveListener(ExitGame);
        }

        public void Show()
        {
            gameObject.SetActive(true);
        }

        private void RestartGame()
        {
            SceneManager.LoadScene(0);
        }

        private void ExitGame()
        {
            Application.Quit();
        }
    }
}