using Assets.Scripts.Domain;
using System.Linq;
using System.Text;
using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;

public class WaveManager : MonoBehaviour
{
    private GameManagerBehaviour _gameManager;
    private PlayerBlobBehaviour _player;
    public PlayerBlobBehaviour Player => _player;
    public GameObject[] Mobs;
    public GameObject[] SpawnedMobs;
    public SpawnWarningBehaviour SpawnWarning;
    private SpawnWarningBehaviour _instantiatedSpawnWarning;

    private readonly int _spawnWarningDelay = 30;
    private Vector3 _spawnLocation;

    private int _delay = 75;
    private int _currentDelay = 25;
    private Wave _wave;

    public TMP_Text ObjectivesText;
    public TMP_Text CoinsText;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        _gameManager = GameManagerBehaviour.Instance;
        _wave = _gameManager.CurrentWave;
        _player = FindFirstObjectByType<PlayerBlobBehaviour>();
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        HandleCompletedQuests();

        _currentDelay++;
        HandleSpawnWarning();
        if (_currentDelay < _delay)
        {
            return;
        }

        DestroySpawnWarning();
        SpawnMobs();
        _currentDelay = 0;
    }

    private void HandleCompletedQuests()
    {
        if (_wave == null)
            return;

        if (_wave.Quest.Objectives.All(q => q.IsCompleted))
        {
            SceneManager.LoadScene(SceneName.QuestSelect);
        }
    }

    private void Update()
    {
        UpdateObjectives();
        UpdateCoins();
    }

    private void UpdateObjectives()
    {
        var objectivesTextBuilder = new StringBuilder();
        foreach (var objective in _wave.Quest.Objectives)
        {
            objectivesTextBuilder.AppendLine($"{objective.NbKilled}/{objective.NbRequiredKills} {objective.Mob.MobName}");
        }
        ObjectivesText.text = objectivesTextBuilder.ToString();
    }

    private void UpdateCoins()
    {
        if (_gameManager == null) return;
        CoinsText.text = _gameManager.NbCoins.ToString();
    }

    private void SpawnMobs()
    {
        foreach (var mob in Mobs)
        {
            Instantiate(mob, _spawnLocation, Quaternion.identity, this.transform);
        }
    }

    private void DestroySpawnWarning()
    {
        Destroy(_instantiatedSpawnWarning.gameObject);
        _instantiatedSpawnWarning = null;
    }

    private void HandleSpawnWarning()
    {
        if (_instantiatedSpawnWarning == null && _currentDelay >= _delay - _spawnWarningDelay)
        {
            var rand = new System.Random();
            _spawnLocation = new Vector3(rand.Next(-10, 10), rand.Next(-10, 10), this.gameObject.transform.position.z);
            _instantiatedSpawnWarning = Instantiate(SpawnWarning, _spawnLocation, Quaternion.identity, this.transform);
        }
    }

    public void RegisterDeath(BaseMobBehaviour mob)
    {
        foreach (var objective in _wave.Quest.Objectives)
        {
            if (objective.Mob.MobName == mob.MobName)
            {
                objective.RegisterKill();
            }
        }
    }
}
