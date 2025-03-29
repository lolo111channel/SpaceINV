using System;
using UnityEngine;
using UnityEngine.InputSystem;

namespace SpaceInv
{
    public class PlayerPauseGame : MonoBehaviour
    {
        public delegate void PlayerPauseGameDelegate(bool gamePause);
        public event PlayerPauseGameDelegate PlayerPauseOrUnpauseGame;

        [SerializeField] private InputActionReference _pauseInput;
        [SerializeField] private InputActionReference _unpauseInput;

        private PlayerInput _playerInput;
        private bool _gamePaused = false;

        public void PauseOrUnpauseGame()
        {
            if (_gamePaused)
            {
                UnpauseGame();
            }
            else
            {
                PauseGame();
            }

        }

        public void PauseGame()
        {
            Time.timeScale = 0.0f;
            _gamePaused = true;
            _playerInput.SwitchCurrentActionMap("UI");

            PlayerPauseOrUnpauseGame?.Invoke(_gamePaused);
        }

        public void UnpauseGame()
        {
            Time.timeScale = 1.0f;
            _gamePaused = false;

            _playerInput.SwitchCurrentActionMap("Gameplay");

            PlayerPauseOrUnpauseGame?.Invoke(_gamePaused);
        }


        private void OnEnable()
        {
            _playerInput = GetComponent<PlayerInput>();


            if (_pauseInput == null || _unpauseInput == null)
            {
                return;
            }

            _pauseInput.action.performed += PauseInputPerformed;
            _unpauseInput.action.performed += PauseInputPerformed;
        }

        private void OnDisable()
        {
            _pauseInput.action.performed -= PauseInputPerformed;
            _unpauseInput.action.performed -= PauseInputPerformed;
        }

        private void PauseInputPerformed(InputAction.CallbackContext context)
        {
            PauseOrUnpauseGame();
        }
    }

}
