using UnityEngine;

namespace SpaceInv
{
    public class StoringOptionValues : MonoBehaviour
    {
        public static StoringOptionValues Instance = null;

        public float MusicVolume = 1.0f;
        public float SoundVolume = 1.0f;
        public Resolution Resolution;
        public bool Fullscreen = true;

        private void OnEnable()
        {
            if (Instance == null)
            { 
                Resolution = Screen.currentResolution;
                Instance = this;
            }
            DontDestroyOnLoad(this);
        }
    }

}
