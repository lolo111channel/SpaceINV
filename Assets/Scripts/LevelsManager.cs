using System;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;


namespace SpaceInv
{
    public class LevelsManager : MonoBehaviour
    {
        public delegate void UnlockedLevels();
        public event UnlockedLevels NewLevelUnlocked;

        public static LevelsManager Instance { get; private set; }
        [SerializeField] private SaveSystem _saveSystem;
        [SerializeField] private List<int> _unlockedLevels = new();

        public void UnlockLevel(int id)
        {
            if (!_unlockedLevels.Contains(id))
            {
                _unlockedLevels.Add(id);
                NewLevelUnlocked?.Invoke();
            }
        }

        public bool CheckIfTheLevelUnlocked(int id)
        {
            if (_unlockedLevels.Contains(id))
            {
                return true;
            }

            return false;
        }


        public int GetLastUnlockedLevel()
        {
            return _unlockedLevels.Last<int>();
        }

        public List<int> GetUnlockedLevels()
        {
            return _unlockedLevels;
        }

        private void Awake()
        {
            if (Instance == null)
            {
                Instance = this;
                DontDestroyOnLoad(gameObject);
            }
           
        }

        private void Start()
        {
            _saveSystem.GameLoaded += GameLoaded;
        }

        private void GameLoaded(SaveObject saveObject)
        {
            _unlockedLevels = saveObject.UnlockedLevels;
        }
    }

}
