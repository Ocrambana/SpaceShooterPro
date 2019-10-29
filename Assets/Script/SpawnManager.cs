using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpawnManager : MonoBehaviour
{
    [SerializeField]
    private float _timeBetweenSpawns = 5f;
    [SerializeField]
    private GameObject _enemyprefab;
    [SerializeField]
    private Transform _enemyContainer;

    private bool _stopSpawning = false;

    void Start()
    {
        StartCoroutine(SpawnEnemy());
    }

    private IEnumerator SpawnEnemy()
    {
        while(!_stopSpawning)
        {
            Vector3 spawnPosition = new Vector3(Random.Range(-9, 9), 7f, 0f);
            Instantiate(_enemyprefab, spawnPosition, Quaternion.identity,_enemyContainer);

            yield return new WaitForSeconds(_timeBetweenSpawns);
        }
    }

    public void OnPlayerDeath()
    {
        _stopSpawning = true;
    }
}
