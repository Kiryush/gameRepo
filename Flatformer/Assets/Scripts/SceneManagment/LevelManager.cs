using UnityEngine;
using UnityEngine.SceneManagement;

namespace SceneManagment
{
    public class LevelManager : MonoBehaviour
    {
        public void MainMenu()
        {
            SceneManager.LoadScene(0);
        }
        public void StartGame()
        {
            SceneManager.LoadScene(1);
        }

        public void ExitGame()
        {
            Application.Quit();
        }
    }
}
