using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy : MonoBehaviour
{
    [SerializeField]
    private float _speed = 4f;

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

    private void OnTriggerEnter(Collider other)
    {
        if(other.tag == "Player")
        {
            Destroy(gameObject);

            other.TryGetComponent<Player>(out Player p);
            p?.Damage();
        }

        if(other.tag == "Laser")
        {
            Destroy(other.gameObject);
            Destroy(gameObject);
        }
    }
}
