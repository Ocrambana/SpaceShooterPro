using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Collider2D))]
public class Enemy : MonoBehaviour
{
    [SerializeField]
    private float _speed = 4f;
    [SerializeField]
    private int _scoreValue = 10;
    [SerializeField]
    private AudioClip _explosionSound;

    private Player _player;
    private Animator _animator;
    private Collider2D _collider;
    private AudioSource _audioSource;

    private void Start()
    {
        _animator = GetComponent<Animator>();
        _collider = GetComponent<Collider2D>();
        _player = FindObjectOfType<Player>();
        _audioSource = GetComponent<AudioSource>();

        if(_player == null)
        {
            Debug.LogError("Enemy._player is NULL");
        }

        if(_animator == null)
        {
            Debug.LogError("Enemy._animator is NULL");
        }
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
            _player?.Damage();
            DeathSequence();
        }
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if(other.tag == "Laser")
        {
            Destroy(other.gameObject);
            _player?.AddToScore(_scoreValue);
            DeathSequence();
        }
    }

    private void DeathSequence()
    {
        _collider.enabled = false;
        _audioSource.clip = _explosionSound;
        _animator.SetTrigger("OnEnemyDeath");
        _audioSource.Play();
        _speed = 0f;

        Destroy(gameObject,2.7f);
    }
}
