using System;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

namespace SpaceInv
{
    public class OptionsUI : MonoBehaviour
    {
        [Header("Fullscreen Setup")]
        [SerializeField] private TMP_Text _fullscreenButtonTxt;
        [SerializeField] private Button _fullscreenButton;
        private bool _isFullscreen = false;

        private void OnEnable()
        {
            _fullscreenButton.onClick?.AddListener(Fullscreen);
            Fullscreen();
        }

        private void OnDisable()
        {
            _fullscreenButton.onClick?.RemoveListener(Fullscreen);
        }

        private void Fullscreen()
        {
            if (_isFullscreen)
            {
                _fullscreenButtonTxt.text = "OFF";
                _isFullscreen = false;
            }
            else
            {
                _fullscreenButtonTxt.text = "ON";
                _isFullscreen = true;
            }


                Screen.fullScreen = _isFullscreen;
        }
    }

}
