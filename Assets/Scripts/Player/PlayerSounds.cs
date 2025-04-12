using System;
using UnityEngine;

namespace SpaceInv
{
    public class PlayerSounds : MonoBehaviour
    {
        [Header("Sounds")]
        [SerializeField] private AudioSource _shootSound;
        [SerializeField] private AudioSource _boostSound;
        [SerializeField] private AudioSource[] _hitSounds;


        [Space(3)]
        [Header("Components")]
        [SerializeField] private Shooting _shooting;
        [SerializeField] private Movement _movement;
        [SerializeField] private HealthComponent _health;

        private System.Random _random = new();

        private void OnEnable()
        {
            _shooting.ObjectShot += ObjectShot;
            _movement.OnObjectIsMoving += OnObjectIsMoving;
            _health.HealthChanged += HealthChanged;
        }

        private void OnDisable()
        {
            _shooting.ObjectShot -= ObjectShot;
            _movement.OnObjectIsMoving -= OnObjectIsMoving;
            _health.HealthChanged -= HealthChanged;
        }

        private void HealthChanged(int currentHealth)
        {
            int randomHitSoundIndex = _random.Next(0, _hitSounds.Length - 1);
            foreach (var hitSound in _hitSounds)
            {
                hitSound.Stop();
            }

            _hitSounds[randomHitSoundIndex].Play();
        }

        private void OnObjectIsMoving()
        {
            if (!_boostSound.isPlaying)
            { 
                _boostSound.Play();
            }
        }

        private void ObjectShot()
        {
            _shootSound.Stop();
            if (!_shootSound.isPlaying)
            {
                _shootSound.Play();
            }
        }
    }

}
