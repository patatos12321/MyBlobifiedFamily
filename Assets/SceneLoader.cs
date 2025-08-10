using UnityEngine;
using UnityEngine.SceneManagement;

public class SceneLoader : MonoBehaviour
{
    public void LoadScene(string sceneName)
    {
        if (sceneName == "Game")
        {
            FindFirstObjectByType<GameManagerBehaviour>().StartGame();
        }
        else 
        {
            SceneManager.LoadScene(sceneName);
        }
    }
}
