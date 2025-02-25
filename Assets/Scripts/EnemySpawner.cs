using System;
using UnityEngine;
using UnityEngine.Serialization;
using Random = UnityEngine.Random;

public class EnemySpawner : MonoBehaviour
{
    [SerializeField] private float spawnInterval; // [SerializeField] - дает возможность изменять private поле в юнити.
    [SerializeField] private Enemy[] enemyPrefabs;
    [SerializeField] private Transform spawnOrigin;
    [SerializeField] private float spawnMinRadius;
    [SerializeField] private float spawnMaxRadius;

    private float _spawnTimer = 0;

    private void Update()
    {
        if (_spawnTimer >= spawnInterval)
        {
            Vector3 spawnPosition = GetRandomPosition();
            int enemyIndex = Random.Range(0, enemyPrefabs.Length);
            Enemy randomEnemy = enemyPrefabs[enemyIndex];

            Enemy createdEnemy = Instantiate(randomEnemy, spawnPosition, Quaternion.identity);
            createdEnemy.SetTarget(spawnOrigin);

            _spawnTimer = 0;
        }

        _spawnTimer += Time.deltaTime;
    }

    private Vector3 GetRandomPosition()
    {
        Vector2 circleMultiplier = Random.insideUnitCircle;
        float xRandomPosition = Random.Range(spawnMinRadius, spawnMaxRadius)
                                * GetSign() * circleMultiplier.x;
        float zRandomPosition = Random.Range(spawnMinRadius, spawnMaxRadius)
                                * GetSign() * circleMultiplier.y;

        Vector3 spawnPosition = new Vector3(xRandomPosition, 0, zRandomPosition) + spawnOrigin.position;

        return spawnPosition;
    }

    private float GetSign()
    {
        float sign = Random.Range(0f, 1f);

        if (sign > 0.5f)
        {
            return -1;
        }
        else
        {
            return 1;
        }
    }
}