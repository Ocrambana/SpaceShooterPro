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

    [Header("Powerups")]
    [SerializeField]
    private float _powerupDuration = 5f;
    [SerializeField]
    private GameObject _tripleLaserPrefab;
    [SerializeField]
    private float _speedMultiplier = 2f;
    [SerializeField]
    private SpriteRenderer _shieldRenderer;

    private float _actualSpeed;
    private float _canFire = -1f; 
    private bool _isTripleLaserActive = false;
    private bool _isShieldActive = false;
    private UIManager _uiManager;
    private int _score = 0;

    void Start()
    {
        transform.position = Vector3.zero;
        _actualSpeed = _speed;
        _uiManager = FindObjectOfType<UIManager>();
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

        transform.Translate(movement * _actualSpeed * Time.deltaTime);
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

        if(_isTripleLaserActive)
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
        if(_isShieldActive)
        {
            _isShieldActive = false;
            _shieldRenderer.enabled = false;
            return;
        }

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
        _isTripleLaserActive = true;

        StartCoroutine(TripleShotDuration());
    }

    private IEnumerator TripleShotDuration()
    {
        yield return new WaitForSeconds(_powerupDuration);

        _isTripleLaserActive = false;
    }

    public void OnSpeedPickup()
    {
        _actualSpeed = _speed * _speedMultiplier;

        StartCoroutine(SpeedPowerupDuration());
    }

    private IEnumerator SpeedPowerupDuration()
    {
        yield return new WaitForSeconds(_powerupDuration);

        _actualSpeed = _speed;
    }

    public void OnShieldPickup()
    {
        _isShieldActive = true;
        _shieldRenderer.enabled = true;
    }

    public void AddToScore(int val)
    {
        if (val < 0)
            return;

        _score += val;
        _uiManager?.UpdateScore(_score);
    }
}
