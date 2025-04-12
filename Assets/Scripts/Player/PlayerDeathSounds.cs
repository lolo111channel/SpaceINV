using UnityEngine;

namespace SpaceInv
{
    public class PlayerDeathSounds : MonoBehaviour
    {
        [SerializeField] private AudioSource[] _deathSounds;
        [SerializeField] private HealthComponent _health;
        private System.Random _random = new();

        private void OnEnable()
        {
            _health.Death += Death;
        }

        private void OnDisable()
        {
            _health.Death -= Death;
        }

        private void Death()
        {
            int randomDeathSoundIndex = _random.Next(0, _deathSounds.Length - 1);
            _deathSounds[randomDeathSoundIndex].Play();
        }
    }

}

