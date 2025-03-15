using System;
using UnityEngine;

namespace SpaceInv
{
    public class PlayerGetDamage : MonoBehaviour
    {
        [SerializeField] private CameraShake _cameraShake;
        private HealthComponent _healthComponent;

        private void OnEnable()
        {
            _healthComponent = GetComponent<HealthComponent>();
            if ( _healthComponent != null )
            {
                _healthComponent.HealthChanged += GetDamage;
            }
        }

        private void GetDamage(int currentHealth)
        {
            if (_cameraShake != null) 
            { 
                _cameraShake.RunShake(0.75f, 5.0f, 1.0f);
            }

        }
    }

}

