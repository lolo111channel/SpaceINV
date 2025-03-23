using UnityEngine;
using UnityEngine.SceneManagement;

namespace SpaceInv
{
    public class GameManager : MonoBehaviour
    {
        [SerializeField] int _nextLevelId = 0;

        private void OnTriggerEnter2D(Collider2D collision)
        {
            if (LevelsManager.Instance != null)
            {
                LevelsManager.Instance.UnlockLevel(_nextLevelId);
            }

            SceneManager.LoadScene(_nextLevelId);
        }
    }

}
