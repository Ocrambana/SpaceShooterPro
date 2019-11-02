using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpawnManager : MonoBehaviour
{
    [Header("Enemies Spawing Settings")]
    [SerializeField]
    private float _timeBetweenEnemy = 5f;
    [SerializeField]
    private GameObject _enemyPrefab;
    [SerializeField]
    private Transform _enemyContainer;

    [Header("Powerups Spawing Settings")]
    [SerializeField]
    private float _timeBetweenPowerups = 7f;
    [SerializeField]
    private List<GameObject> _powerupPrefabs;

    private bool _stopSpawning = false;

    void Start()
    {
        StartCoroutine(SpawnEnemy());
        StartCoroutine(SpawnPowerUp());
    }

    private IEnumerator SpawnEnemy()
    {
        while(!_stopSpawning)
        {
            Vector3 spawnPosition = new Vector3(Random.Range(-9, 9), 7f, 0f);
            Instantiate(_enemyPrefab, spawnPosition, Quaternion.identity,_enemyContainer);

            yield return new WaitForSeconds(_timeBetweenEnemy);
        }
    }

    private IEnumerator SpawnPowerUp()
    {
        GameObject powerupPrefab;
        int randomPoweup;

        while(!_stopSpawning)
        {
            randomPoweup = Random.Range(0, _powerupPrefabs.Count);
            powerupPrefab = _powerupPrefabs[randomPoweup];

            Vector3 spawnPosition = new Vector3(Random.Range(-8, 8), 7f, 0f);
            Instantiate(powerupPrefab, spawnPosition, Quaternion.identity);

            yield return new WaitForSeconds(_timeBetweenPowerups);
        }
    }

    public void OnPlayerDeath()
    {
        _stopSpawning = true;
    }
}
