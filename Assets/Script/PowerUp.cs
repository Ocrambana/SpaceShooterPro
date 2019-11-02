using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PowerUp : MonoBehaviour
{
    private enum PowerupType
    {
        TripleShot,
        Speed,
        Shield
    };

    [SerializeField]
    private float _speed = 3f;
    [SerializeField]
    private PowerupType _powerupType;

    private Vector3 movementDirection;

    private void Start()
    {
        CalculateMovementDirection();

        Destroy(gameObject, 6f);
    }

    private void CalculateMovementDirection()
    {
        float xComponent = Random.Range(-3f, 3f);
        float yComponent = Random.Range(-5f, -1f);
        movementDirection = new Vector3(xComponent, yComponent, 0f);
        movementDirection.Normalize();
    }

    void Update()
    {
        Move();

        if (transform.position.y < -5f)
        {
            Destroy(gameObject);
        }
    }

    private void Move()
    {
        Vector3 movement = movementDirection * _speed * Time.deltaTime;
        float newX = transform.position.x + movement.x;

        if (newX < -9f || newX > 9f)
        {
            movementDirection = Vector3.Reflect(movementDirection, Vector3.right);
            movement = Vector3.Reflect(movement, Vector3.right);
        }

        transform.Translate(movement);
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.tag == "Player")
        {
            collision.TryGetComponent<Player>(out Player p);

            ApplyPowerup(p);

            Destroy(gameObject);
        }
    }

    private void ApplyPowerup(Player player)
    {
        switch(_powerupType)
        {
            case PowerupType.TripleShot:
                player?.OnTripleLaserPickup();
                break;
            case PowerupType.Speed:
                player.OnSpeedPickup();
                break;
            default:
                Debug.LogError("Powerup type unknown");
                break;
        }
    }
}
