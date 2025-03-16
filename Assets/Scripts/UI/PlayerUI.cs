using System;
using TMPro;
using UnityEngine;


namespace SpaceInv
{
    public class PlayerUI : MonoBehaviour
    {
        public Player Player;

        [SerializeField] private TMP_Text _healthCounterTxt;


        private void OnEnable()
        {
            if (Player == null)
            {
                return;
            }

            if (Player.HealthComponent == null)
            {
                return;
            }

            Player.HealthComponent.HealthChanged += HealthChanged;

        }

        private void HealthChanged(int currentHealth)
        {
            if (currentHealth < 0)
            {
                _healthCounterTxt.text = $"Health: { 0 }";
                return;
            }

            _healthCounterTxt.text = $"Health: { currentHealth }";
        }
    }

}
