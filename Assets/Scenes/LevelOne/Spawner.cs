using System.Collections;
using System.Collections.Generic;
using UnityEngine;
public enum SpawnModes
{
    Fixed,
    Random,
}
public class Spawner : MonoBehaviour
{
    [Header("Settings")]
    [SerializeField] private SpawnModes spawnMode = SpawnModes.Fixed;
    [SerializeField] private int enemyCount = 10;
    [SerializeField] private GameObject textGO;

    [Header("FixedDelay")]
    [SerializeField] private float delayBtwSpawns;

    [Header("RandomDelay")]
    [SerializeField] private float minRandomDelay;
    [SerializeField] private float maxRandomDelay;

    private float _spawnTimer;
    private int _enemiesSpawned;

    private ObjectPooler _pooler;
    private void Start()
    {
        _pooler = GetComponent<ObjectPooler>();
    }
    private void Update()
    {
        _spawnTimer -= Time.deltaTime;
        if(_spawnTimer <= 0)
        {
            _spawnTimer = GetSpawnDelay();
            if(_enemiesSpawned < enemyCount)
            {
                _enemiesSpawned++;
                SpawnEnemy();
            }
        }
    }
    private void SpawnEnemy()
    {
        GameObject newInstance = _pooler.GetInstanceFromPool();
        newInstance.SetActive(true);
    }
    private float GetSpawnDelay()
    {
        float delay = 0f;
        if(spawnMode == SpawnModes.Fixed)
        {
            delay = delayBtwSpawns;
        }
        else
        {
            delay = GetRandomDelay();
        }
        return delay;
    }
    private float GetRandomDelay()
    {
        return Random.Range(minRandomDelay, maxRandomDelay);
    }
}
