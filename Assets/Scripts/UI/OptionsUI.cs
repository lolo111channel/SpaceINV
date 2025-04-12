using System;
using System.Collections.Generic;
using System.Linq;
using TMPro;
using UnityEngine;
using UnityEngine.Audio;
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


        [SerializeField] private AudioMixer _audioMixer;

        [SerializeField] private Slider _musicSlider;
        [SerializeField] private TMP_Text _musicSliderText;

        [SerializeField] private Slider _sfxSlider;
        [SerializeField] private TMP_Text _sfxSliderText;

        private bool _isFullscreen = false;


        private Resolution[] _resolutions;
        private List<String> _textResolutions = new();
        private int _currentResolutionID = 0;

        public static float MusicVolume = 0.0f;
        public static float PreviouseMusicVolume = 0.0f;

        public static float SFXVolume = 0.0f;
        public static float PreviouseSFXVolume = 0.0f;

        public void SetMusicVolume(Slider slider)
        {
            MusicVolume = slider.value;
        }

        public void SetSFXVolume(Slider slider)
        {
            SFXVolume = slider.value;
        }

        private void OnEnable()
        {
            _musicSlider.value = MusicVolume;
            _sfxSlider.value = SFXVolume;

            _fullscreenButton.onClick?.AddListener(Fullscreen);
            _resolutionDropdown.onValueChanged.AddListener(SetResolution);

            _resolutions = Screen.resolutions;
            List<Resolution> _newResolutions = new();
            foreach (var res in _resolutions)
            {
                string text = $"{res.width} x {res.height}";

                if (_textResolutions.Contains(text))
                {
                    continue;
                }

                _newResolutions.Add(res);
                _textResolutions.Add(text);
            }

            _resolutionDropdown.ClearOptions();
            _resolutionDropdown.AddOptions(_textResolutions);
            _resolutions = _newResolutions.ToArray();


            Resolution currentResolution = Screen.currentResolution;
            _currentResolutionID = _textResolutions.FindIndex(x => x == $"{currentResolution.width} x {currentResolution.height}");
            
            if (_currentResolutionID <= -1)
            {
                _currentResolutionID = _textResolutions.Count - 1;
            }


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

        private void Update()
        {

            _musicSliderText.text = $"{ChangeVolumeDBToPercentage(_musicSlider.minValue, _musicSlider.maxValue, _musicSlider.value)}%";
            _sfxSliderText.text = $"{ChangeVolumeDBToPercentage(_sfxSlider.minValue, _sfxSlider.maxValue, _sfxSlider.value)}%";

            if (!Mathf.Approximately(MusicVolume, PreviouseMusicVolume))
            {
                _audioMixer.SetFloat("music", MusicVolume);
            }

            if (!Mathf.Approximately(SFXVolume, PreviouseSFXVolume))
            {
                _audioMixer.SetFloat("sfx", SFXVolume);
            }

            PreviouseMusicVolume = MusicVolume;
            PreviouseSFXVolume = SFXVolume;
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


        private int ChangeVolumeDBToPercentage(float min, float max, float currentValue)
        {
            float newMin = 0.0f;
            float newMax = 0.0f;
            float newCurrentVal = 0.0f;

            if (min < 0)
            {
                newMin = min + (min * -1);
                newMax = max + (min * -1);
                newCurrentVal = currentValue + (min * -1);
            }

            float percentage = (newCurrentVal / newMax) * 100f;

            return Mathf.RoundToInt(percentage);

        }
    }

}
