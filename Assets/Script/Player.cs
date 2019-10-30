using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour
{
    [Header("Speed and lives")]
    [SerializeField]
    private float _speed = 3.5f;
    [SerializeField]
    private int _lives = 3;

    [Header("Shooting settings")]
    [SerializeField]
    private GameObject _laserPrefab;
    [SerializeField]
    private Vector3 _projectileOffset = Vector3.zero;
    [SerializeField]
    private float _fireRate = 0.5f;

    [Header("Triple Laser")]
    [SerializeField]
    private GameObject _tripleLaserPrefab;
    [SerializeField]
    private float _tripleLaserDuration = 5f;

    private float _canFire = -1f; 
    private bool isTripleLaserActive = false;

    void Start()
    {
        transform.position = Vector3.zero;   
    }
    
    void Update()
    {
        CalculateMovement();
        ClampPositionInsideBorder();

        if (Input.GetKeyDown(KeyCode.Space) && Time.time > _canFire)
        {
            FireLaser();
        }
    }

    private void CalculateMovement()
    {
        float horizontalInput = Input.GetAxis("Horizontal");
        float verticalInput = Input.GetAxis("Vertical");

        Vector3 movement = new Vector3(horizontalInput, verticalInput, 0f);

        transform.Translate(movement * _speed * Time.deltaTime);
    }

    private void ClampPositionInsideBorder()
    {
        float clampedY = Mathf.Clamp(transform.position.y, -3.8f, 0f);
        transform.position = new Vector3(transform.position.x, clampedY, 0);

        if (transform.position.x > 11 || transform.position.x < -11)
        {
            transform.position = new Vector3(-transform.position.x, transform.position.y, 0);
        }
    }

    private void FireLaser()
    {
        _canFire = Time.time + _fireRate;
        Vector3 spawnPostion = transform.position + _projectileOffset;

        if(isTripleLaserActive)
        {
            Instantiate(_tripleLaserPrefab, spawnPostion, Quaternion.identity);
        }
        else
        {
            Instantiate(_laserPrefab, spawnPostion, Quaternion.identity);
        }
    }

    public void Damage()
    {
        _lives--;

        if(_lives < 1)
        {
            Destroy(gameObject);
        }
    }

    private void OnDestroy()
    {
        SpawnManager sm = FindObjectOfType<SpawnManager>();
        sm?.OnPlayerDeath();

        if(sm == null)
        {
            Debug.LogError("Spawn Manager not found");
        }
    }

    public void OnTripleLaserPickup()
    {
        isTripleLaserActive = true;

        StartCoroutine(TripleShotDuration());
    }

    private IEnumerator TripleShotDuration()
    {
        yield return new WaitForSeconds(_tripleLaserDuration);

        isTripleLaserActive = false;
    }
}
