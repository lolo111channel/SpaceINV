using System;
using System.Collections.Generic;
using System.Linq;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

namespace SpaceInv
{
    public class OptionsUI : MonoBehaviour
    {
        [Header("Fullscreen UI EL Setup")]
        [SerializeField] private TMP_Text _fullscreenButtonTxt;
        [SerializeField] private Button _fullscreenButton;

        [Header("Resolution UI EL Setup")]
        [SerializeField] private TMP_Dropdown _resolutionDropdown;

        
        private bool _isFullscreen = false;


        private Resolution[] _resolutions;
        private List<String> _textResolutions = new();
        private int _currentResolutionID = 0;


        private void OnEnable()
        {
            _fullscreenButton.onClick?.AddListener(Fullscreen);
            _resolutionDropdown.onValueChanged.AddListener(SetResolution);

            _resolutions = Screen.resolutions;
            foreach (var res in _resolutions)
            {
                string text = $"{res.width} x {res.height}";

                if (_textResolutions.Contains(text))
                {
                    //return;
                }


                _textResolutions.Add(text);
            }

            _resolutionDropdown.ClearOptions();
            _resolutionDropdown.AddOptions(_textResolutions);

            Resolution currentResolution = Screen.currentResolution;
            _currentResolutionID = _textResolutions.FindIndex(x => x == $"{currentResolution.width} x {currentResolution.height}");

            _resolutionDropdown.value = _currentResolutionID;
        }


        private void OnDisable()
        {
            _fullscreenButton.onClick?.RemoveListener(Fullscreen);
        }

        private void Awake()
        {

            Fullscreen();
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
        private void SetResolution(int index)
        {
            Resolution currentResolution = _resolutions[index];

            Screen.SetResolution(currentResolution.width, currentResolution.height, _isFullscreen);
        }
    }

}
