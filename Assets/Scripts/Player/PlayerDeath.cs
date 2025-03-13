using System;
using UnityEngine;

namespace SpaceInv
{ 

    public class PlayerDeath : MonoBehaviour
    {
        private HealthComponent healthComponent;

        private void OnEnable()
        {
            healthComponent = GetComponent<HealthComponent>();

            healthComponent.Death += Death;
        }

        private void Death()
        {
            Destroy(gameObject);
        }
    }

}

