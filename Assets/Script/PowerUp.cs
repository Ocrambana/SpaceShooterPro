using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PowerUp : MonoBehaviour
{
    [SerializeField]
    private float _speed = 3f;

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
        CalculateMovement();

        if (transform.position.y < -5f)
        {
            Destroy(gameObject);
        }
    }

    private void CalculateMovement()
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
        if(collision.tag == "Player")
        {
            collision.TryGetComponent<Player>(out Player p);
            p?.OnTripleLaserPickup();

            Destroy(gameObject);
        }
    }
}
