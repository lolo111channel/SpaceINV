using UnityEngine;
using UnityEngine.SceneManagement;

namespace SpaceInv
{
    public class GameManager : MonoBehaviour
    {
        [SerializeField] int _nextLevelId = 0;
        [SerializeField] private bool _canSave = true;

        private void OnTriggerEnter2D(Collider2D collision)
        {
            if (LevelsManager.Instance != null)
            {
                LevelsManager.Instance.UnlockLevel(_nextLevelId);
            }

            if (SaveSystem.Instance != null && _canSave)
            {
                SaveSystem.Instance.Save();
            }

            SceneManager.LoadScene(_nextLevelId);
        }
    }

}
