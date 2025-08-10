using UnityEngine;
using UnityEngine.SceneManagement;

public class GameManagerBehaviour : MonoBehaviour
{
    public static GameManagerBehaviour Instance;

    private int _nbCoins= 0;
    public int NbCoins => _nbCoins;

    public void Defeat()
    {
        SceneManager.LoadScene("Defeat");
    }

    public void StartGame()
    {
        ResetGear();
        SceneManager.LoadScene("Game");
    }

    private void ResetGear()
    {
        _nbCoins = 0;
    }

    private void Awake()
    {
        if (Instance != null)
        {
            Destroy(gameObject);
            return;
        }

        Instance = this;
        DontDestroyOnLoad(gameObject);
    }

    public void AddMoney(int amount)
    {
        _nbCoins += amount;
    }
}
