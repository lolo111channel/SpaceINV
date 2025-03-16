using UnityEngine;

namespace SpaceInv
{
    public class HealthComponent : MonoBehaviour
    {
        public delegate void HealthChangedEvent(int currentHealth);
        public delegate void HealthEquelZero();

        public event HealthChangedEvent HealthChanged;
        public event HealthEquelZero Death;

        public int Health = 10;
        public int MaxHealth = 10;

        public void TakeDamage(int value)
        {
            Health -= value;
            HealthChanged?.Invoke(Health);

            if (Health <= 0)
            {
                Death?.Invoke();
            }

        }
    }

}
