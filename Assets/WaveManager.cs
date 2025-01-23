using System.Collections.Generic;
using UnityEngine;

public class WaveManager : MonoBehaviour
{
    private PlayerBlobBehaviour _player;
    public PlayerBlobBehaviour Player => _player;
    public GameObject[] Mobs;
    public SpawnWarningBehaviour SpawnWarning;
    private SpawnWarningBehaviour instantiatedSpawnWarning;
    private int spawnWarningDelay = 30;
    private Vector3 spawnLocation;
    
    private int delay = 100;
    private int currentDelay = 100;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        _player = FindFirstObjectByType<PlayerBlobBehaviour>();
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        currentDelay++;
        InstantiateSpawnWarning();
        if (currentDelay < delay)
        {
            return;
        }

        DestroySpawnWarning();
        SpawnMobs();
        currentDelay = 0;
    }

    private void SpawnMobs()
    {
        foreach (var mob in Mobs)
        {
            var instantiatedMob = Instantiate(mob, spawnLocation, Quaternion.identity, this.transform);
        }
    }

    private void DestroySpawnWarning()
    {
        Destroy(instantiatedSpawnWarning.gameObject);
        instantiatedSpawnWarning = null;
    }

    private void InstantiateSpawnWarning()
    {
        var rand = new System.Random();
        spawnLocation = new Vector3(rand.Next(-10, 10), rand.Next(-10, 10), this.gameObject.transform.position.z);

        if (instantiatedSpawnWarning == null && currentDelay >= delay - spawnWarningDelay)
        {
            instantiatedSpawnWarning = Instantiate(SpawnWarning, spawnLocation, Quaternion.identity, this.transform);
        }
    }
}
