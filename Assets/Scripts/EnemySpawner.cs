using System;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Serialization;
using Random = UnityEngine.Random;

public class EnemySpawner : MonoBehaviour
{
    [SerializeField] private GameManager gameManager;
    [SerializeField] private float spawnInterval; // [SerializeField] - дает возможность изменять private поле в юнити.
    [SerializeField] private Enemy[] enemyPrefabs;
    [SerializeField] private Transform spawnOrigin;

    [SerializeField] private float minSpawnRadius = 10f;
    [SerializeField] private float maxSpawnRadius = 20f;
    [SerializeField] private float exclusionRadius = 5f;

    private float _spawnTimer = 0;
    private List<Enemy> _enemies = new();
    private bool _canSpawn = true;

    public void SetSpawnState(bool flag)
    {
        _canSpawn = flag;
    }

    private void Update()
    {
        if (_canSpawn && _spawnTimer >= spawnInterval)
        {
            Vector3 spawnPosition = GetValidSpawnPosition();
            SpawnEnemy(spawnPosition);
            _spawnTimer = 0;
        }

        _spawnTimer += Time.deltaTime;
    }

    private Vector3 GetValidSpawnPosition()
    {
        Vector3 spawnCenter = spawnOrigin.position;

        while (true)
        {
            Vector2 randomCircle = Random.insideUnitCircle.normalized;
            float randomRadius = Random.Range(minSpawnRadius, maxSpawnRadius);

            Vector3 candidatePosition = new Vector3(
                spawnCenter.x + randomCircle.x * randomRadius,
                0,
                spawnCenter.z + randomCircle.y * randomRadius
            );
            float distanceToCenter = Vector3.Distance(candidatePosition, spawnCenter);

            if (distanceToCenter >= exclusionRadius && distanceToCenter <= maxSpawnRadius)
            {
                return candidatePosition;
            }
        }
    }

    private void SpawnEnemy(Vector3 spawnPosition)
    {
        int enemyIndex = Random.Range(0, enemyPrefabs.Length);
        Enemy randomEnemy = enemyPrefabs[enemyIndex];

        Enemy createdEnemy = Instantiate(randomEnemy, spawnPosition, Quaternion.identity);
        createdEnemy.SetTarget(spawnOrigin);
        createdEnemy.OnDie += CreatedEnemyOnOnDie;

        _enemies.Add(createdEnemy);
    }

    private void CreatedEnemyOnOnDie(Enemy enemy)
    {
        gameManager.IncreaseCoins(1);
        _enemies.Remove(enemy);
    }
}