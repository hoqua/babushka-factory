using UnityEngine;
using UnityEngine.SceneManagement;

namespace MainMenu
{
    public class StartButton : MonoBehaviour
    {
    
        public void StartGame()
        {
            SceneManager.LoadScene("Game");
        }
    }
}
