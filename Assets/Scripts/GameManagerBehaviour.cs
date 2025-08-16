using Assets.Scripts.Domain;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameManagerBehaviour : MonoBehaviour
{
    public static GameManagerBehaviour Instance;

    private int _nbCoins= 0;
    private readonly List<Wave> _waves = new ();
    public int NbCoins => _nbCoins;
    public int WaveNumber => _waves.Count;
    public Wave CurrentWave => _waves.LastOrDefault();

    public void Defeat()
    {
        SceneManager.LoadScene(SceneName.Defeat);
    }

    public void StartGame()
    {
        ResetWaves();
        ResetGear();

        SceneManager.LoadScene(SceneName.QuestSelect);
    }

    private void ResetWaves()
    {
        Debug.Log("GameManagerBehavior resets all quests");
        _waves.Clear();
    }

    private void ResetGear()
    {
        _nbCoins = 0;
    }

    private void Awake()//singleton
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

    public void DepartOnQuest(Quest quest)
    {
        Debug.Log("GameManagerBehavior goes on quest");
        _waves.Add(new Wave(WaveNumber+1, quest));
        SceneManager.LoadScene(SceneName.Game);
    }
}
