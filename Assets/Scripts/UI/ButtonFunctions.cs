using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;


namespace SpaceInv
{
    public class ButtonFunctions : MonoBehaviour
    {

        public void Continue()
        {
            if (LevelsManager.Instance == null)
            {
                return;
            }

            SceneManager.LoadScene(LevelsManager.Instance.GetLastUnlockedLevel());
        }

        public void NewGame()
        {
            SceneManager.LoadScene(1);
        }

        public void QuitGame()
        {
            Application.Quit();
        }
    }

}
