using System;
using Unity.VisualScripting;
using UnityEngine;

namespace SpaceInv
{
    public class Fuel : MonoBehaviour
    {
        public delegate void FuelDelegate(float fuel);
        public event FuelDelegate CurrentFuelChanged;
        
        [SerializeField] private float _maxFuel = 100.0f;
        [SerializeField] private float _currentFuel = 1.0f;

        [SerializeField] private float _fuelRegenerationSpeed = 0.25f;
        [SerializeField] private float _fuelLossRate = 0.25f;

        [SerializeField] private float _timeToStartRegeneration = 0.15f;

        private bool _canRegeneration = false;
        private float _timeToStartRegenerationTime = 0.0f;

        private Movement _movement;

        public bool IsFuelFull()
        {
            if (_currentFuel >= _maxFuel)
            {
                return true;
            }

            return false;
        }

        public bool IsFuelEquelZero()
        {
            if (_currentFuel <= 0)
            {
                return true;
            }

            return false;
        }

        public float GetMaxFuel() => _maxFuel;

        private void OnObjectIsMoving()
        {
            if (IsFuelEquelZero())
            {
                return;
            }

            _currentFuel -= _fuelLossRate * Time.deltaTime;
            _canRegeneration = false;
            _timeToStartRegenerationTime = 0.0f;

            CurrentFuelChanged?.Invoke(_currentFuel);
        }

        private void OnEnable()
        {
            _movement = GetComponent<Movement>();
            _currentFuel = _maxFuel;

            if (_movement != null)
            {
                _movement.OnObjectIsMoving += OnObjectIsMoving;
            }
        }

        private void Update()
        {
            if (!_canRegeneration)
            { 
                _timeToStartRegenerationTime += Time.deltaTime;
                if (_timeToStartRegenerationTime >= _timeToStartRegeneration)
                {
                    _canRegeneration = true;
                    _timeToStartRegenerationTime = 0.0f;
                }
            }

            if (_currentFuel <= _maxFuel && _canRegeneration)
            {
                _currentFuel += _fuelRegenerationSpeed * Time.deltaTime;
                CurrentFuelChanged?.Invoke(_currentFuel);
            }
        }

    }

}

