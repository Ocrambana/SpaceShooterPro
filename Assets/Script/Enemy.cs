using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy : MonoBehaviour
{
    [SerializeField]
    private float _speed = 4f;
    [SerializeField]
    private int _scoreValue = 10;

    private Player _player;

    private void Start()
    {
        _player = FindObjectOfType<Player>();
    }

    void Update()
    {
        CalculateMovement();

        if (transform.position.y < -5f)
        {
            RandomiseSpawnPosition();
        }
    }

    private void CalculateMovement()
    {
        Vector3 movement = Vector3.down * _speed * Time.deltaTime;
        transform.Translate(movement);
    }

    private void RandomiseSpawnPosition()
    {
        float randomX = Random.Range(-9, 9);
        Vector3 respawnPos = new Vector3(randomX, 7f, 0f);
        transform.position = respawnPos;
    }

    private void OnCollisionEnter2D(Collision2D other)
    {
        if(other.gameObject.tag == "Player")
        {
            Destroy(gameObject);

            _player?.Damage();
        }
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if(other.tag == "Laser")
        {
            Destroy(other.gameObject);
            _player?.AddToScore(_scoreValue);
            Destroy(gameObject);
        }
    }
}
