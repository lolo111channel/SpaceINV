using System;
using UnityEngine;

namespace SpaceInv
{
    public class EnemyDeath : MonoBehaviour
    {
        private HealthComponent _healthComponent;

        private void OnEnable()
        {
            _healthComponent = GetComponent<HealthComponent>();
            _healthComponent.Death += Death;
        }

        private void Death()
        {
            Destroy(gameObject);
        }
    }
}

