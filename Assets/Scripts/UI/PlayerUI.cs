using System;
using TMPro;
using UnityEngine;


namespace SpaceInv
{
    public class PlayerUI : MonoBehaviour
    {
        public Player Player;

        [SerializeField] private TMP_Text _healthCounterTxt;
        [SerializeField] private TMP_Text _fuelCounterTxt;

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

            if (Player.Fuel == null)
            {
                return;
            }

            Player.HealthComponent.HealthChanged += HealthChanged;
            Player.Fuel.CurrentFuelChanged += CurrentFuelChanged;
        }

        private void CurrentFuelChanged(float fuel)
        {
            int roundedFuel = Mathf.RoundToInt(fuel);

            _fuelCounterTxt.text = $"Fuel: {roundedFuel}/{Player.Fuel.GetMaxFuel()}";
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
