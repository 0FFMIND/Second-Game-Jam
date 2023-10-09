using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Random = UnityEngine.Random;

public enum SpawnModes
{
    Fixed,
    Random
}

public class Spawner : MonoBehaviour
{
    [Header("Settings")]
    [SerializeField] private SpawnModes spawnMode = SpawnModes.Fixed;
    [SerializeField] private int enemyCount = 10;
    [SerializeField] private float delayBtwWaves = 1f;

    [SerializeField] private float minRandomDelay;
    [SerializeField] private float maxRandomDelay;

    [Header("Poolers")] 
    [SerializeField] private ObjectPooler enemyWave1Pooler;
    [SerializeField] private ObjectPooler enemyWave2Pooler;
    [SerializeField] private ObjectPooler enemyWave3Pooler;

    
    private float _spawnTimer;
    private int _enemiesSpawned;
    private int _enemiesRamaining;
    

    private void Start()
    {
        _enemiesRamaining = enemyCount;
    }

    private void Update()
    {
        _spawnTimer -= Time.deltaTime;
        if (_spawnTimer < 0)
        {
            _spawnTimer = GetSpawnDelay();
            if (_enemiesSpawned < enemyCount)
            {
                _enemiesSpawned++;
                SpawnEnemy();
            }
        }
    }

    private void SpawnEnemy()
    {
        GameObject newInstance = GetPooler().GetInstanceFromPool();
        Enemy enemy = newInstance.GetComponent<Enemy>();

        enemy.transform.localPosition = transform.position;
        newInstance.SetActive(true);
    }

    private float GetSpawnDelay()
    {
        float delay = 0f;
        if (spawnMode == SpawnModes.Fixed)
        {
            delay = delayBtwWaves;
        }
        else
        {
            delay = GetRandomDelay();
        }

        return delay;
    }
    
    private float GetRandomDelay()
    {
        float randomTimer = Random.Range(minRandomDelay, maxRandomDelay);
        return randomTimer;
    }

    private ObjectPooler GetPooler()
    {
        int currentWave = 0;
        if (currentWave <= 1) // 1- 10
        {
            return enemyWave1Pooler;
        }

        if (currentWave > 1 && currentWave <= 2) // 11- 20
        {
            return enemyWave2Pooler;
        }
        
        if (currentWave > 2 && currentWave <= 3) // 21- 30
        {
            return enemyWave3Pooler;
        }
        


        return null;
    }
    
    private IEnumerator NextWave()
    {
        yield return new WaitForSeconds(delayBtwWaves);
        _enemiesRamaining = enemyCount;
        _spawnTimer = 0f;
        _enemiesSpawned = 0;
    }
    
    private void RecordEnemy(Enemy enemy)
    {
        //_enemiesRamaining--;
        //if (_enemiesRamaining <= 0)
        //{
        //    OnWaveCompleted?.Invoke();
        //    StartCoroutine(NextWave());
        //}
    }
}
