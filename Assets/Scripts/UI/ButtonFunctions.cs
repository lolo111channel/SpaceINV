using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;


namespace SpaceInv
{
    public class ButtonFunctions : MonoBehaviour
    {

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
