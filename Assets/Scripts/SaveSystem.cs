using System;
using System.Collections.Generic;
using System.IO;
using UnityEngine;

namespace SpaceInv
{
    public class SaveSystem : MonoBehaviour
    {
        public delegate void SaveSystemDelegate(SaveObject saveObject);
        public event SaveSystemDelegate GameLoaded;

        public static SaveSystem Instance = null;


        public void Load()
        {
            string path = Application.dataPath + "/" + "save.json";
            if (!File.Exists(path))
            {
                return;
            }

            SaveObject saveObject = new();
            string json = File.ReadAllText(path);;
            JsonUtility.FromJsonOverwrite(json, saveObject);

            Debug.Log("Game Loaded");
            foreach (var el in saveObject.UnlockedLevels)
            {
                Debug.Log(el);
            }
            GameLoaded?.Invoke(saveObject);
        }

        public void Save()
        {
            if (LevelsManager.Instance == null)
            {
                return;
            }


            SaveObject saveObject = new();
            saveObject.UnlockedLevels = LevelsManager.Instance.GetUnlockedLevels();

            string json = JsonUtility.ToJson(saveObject);
            File.WriteAllText(Application.dataPath + "/" + "save.json", json);

            Debug.Log("Game Saved");
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
            Load();
        }

    }

    [Serializable]
    public class SaveObject
    {
        public List<int> UnlockedLevels = new();
    }


}

