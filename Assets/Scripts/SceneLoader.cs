using Assets.Scripts.Domain;
using UnityEngine;
using UnityEngine.SceneManagement;

public class SceneLoader : MonoBehaviour
{
    public void LoadNewGame()
    {
        GameManagerBehaviour.Instance.StartGame();
    }

    public void LoadTitle()
    {
        SceneManager.LoadScene(SceneName
            .Title);
    }
}
