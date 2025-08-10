using System;
using System.Diagnostics;
using TMPro;
using UnityEngine;

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

    private int _delay = 150;
    private int _currentDelay = 100;

    private Stopwatch _timer = new Stopwatch();
    public TMP_Text TimerText;
    public TMP_Text CoinsText;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
       _gameManager = FindFirstObjectByType<GameManagerBehaviour>();
        _player = FindFirstObjectByType<PlayerBlobBehaviour>();
        _timer.Start();
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        _currentDelay++;
        InstantiateSpawnWarning();
        if (_currentDelay < _delay)
        {
            return;
        }

        DestroySpawnWarning();
        SpawnMobs();
        _currentDelay = 0;
    }
    private void Update()
    {
        UpdateTimer();
        UpdateCoins();
    }

    private void UpdateTimer()
    {
        var minutes = Math.Floor(_timer.Elapsed.TotalMinutes);
        TimerText.text = $"{minutes}:{_timer.Elapsed.Seconds:00}";
    }

    private void UpdateCoins()
    {
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

    private void InstantiateSpawnWarning()
    {
        if (_instantiatedSpawnWarning == null && _currentDelay >= _delay - _spawnWarningDelay)
        {
            var rand = new System.Random();
            _spawnLocation = new Vector3(rand.Next(-10, 10), rand.Next(-10, 10), this.gameObject.transform.position.z);
            _instantiatedSpawnWarning = Instantiate(SpawnWarning, _spawnLocation, Quaternion.identity, this.transform);
        }
    }
}
