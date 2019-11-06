using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Asteroid : MonoBehaviour
{
    [SerializeField]
    private float _rotationSpeed = 5f;
    [SerializeField]
    private GameObject _explosion;

    private SpawnManager _spawnManager;
    private Collider2D _collider;

    private void Start()
    {
        _spawnManager = FindObjectOfType<SpawnManager>();

        if(_spawnManager == null)
        {
            Debug.LogError("Asteroid: SpawnManager is null");
        }
        _collider = GetComponent<Collider2D>();
    }

    void Update()
    {
        transform.Rotate(Vector3.forward, _rotationSpeed * Time.deltaTime);   
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if(collision.tag == "Laser")
        {
            _collider.enabled = false;
            Destroy(collision.gameObject);
            GameObject explosion = Instantiate(_explosion, transform.position, Quaternion.identity);
            Destroy(explosion, 3f);
            _spawnManager?.StartSpawning();
            Destroy(gameObject, 1.3f);
        }
    }
}
