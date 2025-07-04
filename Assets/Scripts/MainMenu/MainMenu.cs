using UnityEngine;
using UnityEngine.SceneManagement;

namespace MainMenu
{
    public class MainMenu : MonoBehaviour
    {
        public void PlayGame()
        {
            SceneManager.LoadScene("GameScene");
        }

        public void QuitGame()
        {
            Application.Quit();
        }
    }
}