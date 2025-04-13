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

        public static float MusicVolume = 1.0f;
        public static float PreviouseMusicVolume = 1.0f;

        public static float SFXVolume = 1.0f;
        public static float PreviouseSFXVolume = 1.0f;

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


            MusicVolume = StoringOptionValues.Instance.MusicVolume;
            SFXVolume = StoringOptionValues.Instance.SoundVolume;

            _musicSlider.value = MusicVolume;
            _sfxSlider.value = SFXVolume;

            Resolution currentResolution = StoringOptionValues.Instance.Resolution;
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
            _isFullscreen = StoringOptionValues.Instance.Fullscreen;
            Screen.fullScreen = _isFullscreen;
            if (_isFullscreen)
            {
                _fullscreenButtonTxt.text = "ON";
            }
            else
            {
                _fullscreenButtonTxt.text = "OFF";
            }
        }

        private void Update()
        {

            _musicSliderText.text = $"{ChangeVolumeDBToPercentage( _musicSlider.value)}%";
            _sfxSliderText.text = $"{ChangeVolumeDBToPercentage(_sfxSlider.value)}%";

            if (!Mathf.Approximately(MusicVolume, PreviouseMusicVolume))
            {
                _audioMixer.SetFloat("music", Mathf.Log10(MusicVolume) * 20);
            }

            if (!Mathf.Approximately(SFXVolume, PreviouseSFXVolume))
            {
                _audioMixer.SetFloat("sfx", Mathf.Log10(SFXVolume) * 20);
            }

            PreviouseMusicVolume = MusicVolume;
            PreviouseSFXVolume = SFXVolume;

            StoringOptionValues.Instance.MusicVolume = MusicVolume;
            StoringOptionValues.Instance.SoundVolume = SFXVolume;
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

            StoringOptionValues.Instance.Fullscreen = _isFullscreen;
            Screen.fullScreen = _isFullscreen;
        }
        private void SetResolution(int index)
        {
            Resolution currentResolution = _resolutions[index];
            StoringOptionValues.Instance.Resolution = currentResolution;

            Screen.SetResolution(currentResolution.width, currentResolution.height, _isFullscreen);
        }


        private int ChangeVolumeDBToPercentage(float currentValue)
        {
        
            float percentage = currentValue * 100;
            return Mathf.RoundToInt(percentage);

        }
    }

}
