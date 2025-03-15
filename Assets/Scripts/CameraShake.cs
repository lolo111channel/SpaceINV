using Unity.Cinemachine;
using UnityEngine;


namespace SpaceInv
{
    public class CameraShake : MonoBehaviour
    {
        private CinemachineBasicMultiChannelPerlin _cinemachineBasicMultiChannelPerlin;
        private bool _isShakeStarted = false;
        private float _shakeTime = 0.0f;
        private float _time = 0.0f;


        public void RunShake(float time, float amplitude, float frequency)
        {
            if (_isShakeStarted) return;

            _isShakeStarted = true;
            _shakeTime = time;
            _cinemachineBasicMultiChannelPerlin.AmplitudeGain = amplitude;
            _cinemachineBasicMultiChannelPerlin.FrequencyGain = frequency;

            _cinemachineBasicMultiChannelPerlin.enabled = true;
        }

        private void OnEnable()
        {
            _cinemachineBasicMultiChannelPerlin = GetComponent<CinemachineBasicMultiChannelPerlin>();
        }

        private void Update()
        {
            if (_isShakeStarted)
            {
                _time += Time.deltaTime;
                if (_time > _shakeTime) 
                { 
                    _isShakeStarted = false;
                    _cinemachineBasicMultiChannelPerlin.enabled = false;
                    _time = 0.0f;
                }

            };
        }
    }
}
