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

        [SerializeField] private GameObject _deathScreen;
        [SerializeField] private GameObject _pauseMenu;

        
        public void UnpauseGameButtonFunc()
        {
            if (Player == null)
            {
                return;
            }

            Player.PlayerPauseGame.UnpauseGame();
        }


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

            if (Player.PlayerPauseGame == null)
            {
                return;
            }



            Player.HealthComponent.HealthChanged += HealthChanged;
            Player.HealthComponent.Death += Death;

            Player.Fuel.CurrentFuelChanged += CurrentFuelChanged;

            Player.PlayerPauseGame.PlayerPauseOrUnpauseGame += PlayerPauseOrUnpauseGame;
        }


        private void OnDisable()
        {
            Player.HealthComponent.HealthChanged -= HealthChanged;
            Player.HealthComponent.Death -= Death;

            Player.Fuel.CurrentFuelChanged -= CurrentFuelChanged;

            Player.PlayerPauseGame.PlayerPauseOrUnpauseGame -= PlayerPauseOrUnpauseGame;
        }

        private void PlayerPauseOrUnpauseGame(bool gamePause)
        {
            _pauseMenu.SetActive(gamePause);
        }


        private void Death()
        {
            _deathScreen.SetActive(true);
        }

        private void CurrentFuelChanged(float fuel)
        {
            int roundedFuel = Mathf.RoundToInt(fuel);

            if (fuel > 100)
            {
                fuel = 100;
            }

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
