using System;
using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

namespace SpaceInv
{
    public class StartALevelButton : MonoBehaviour
    {
        [SerializeField] private int _levelId = 3;
        [SerializeField] private string _levelName = "Level 1";
        [SerializeField] private TMP_Text _text;

        private bool _unlocked = false;
        private Button _btn;


        private void Start()
        {
            NewLevelUnlocked();
            LevelsManager.Instance.NewLevelUnlocked += NewLevelUnlocked;

            _btn = GetComponent<Button>();
            if (_btn != null )
            {
                _btn.onClick.AddListener(StartALevel);
            }
        }

        private void NewLevelUnlocked()
        {
            if (LevelsManager.Instance.CheckIfTheLevelUnlocked(_levelId))
            {
                _unlocked = true;
                _text.text = _levelName;
                return;
            }

            _text.text = "Locked";
        }

        private void StartALevel()
        {
            if (_unlocked)
            {
                SceneManager.LoadScene(_levelId);
            }
        }
    }

}
