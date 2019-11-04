using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class UIManager : MonoBehaviour
{
    [SerializeField]
    private TextMeshProUGUI _scoreText;
    [SerializeField]
    private Image _livesImage;
    [SerializeField]
    private List<Sprite> _liveSprites;
    [SerializeField]
    private TextMeshProUGUI _gameOverLabel;
    [SerializeField]
    private float _flickerSpeed = 0.2f;

    private GameManager _gameManager;

    void Start()
    {
        UpdateLives(_liveSprites.Count - 1);
        UpdateScore(0);
        _gameOverLabel?.gameObject.SetActive(false);
        _gameManager = FindObjectOfType<GameManager>();
    }

    public void UpdateScore(int val)
    {
        _scoreText.text = val.ToString();
    }

    public void UpdateLives(int currentLives)
    {
        if (currentLives >= _liveSprites.Count)
            return;

        _livesImage.sprite = _liveSprites[currentLives];
    }

    public void GameOver()
    {
        _gameOverLabel?.gameObject.SetActive(true);
        StartCoroutine(FlickerGameOverText());
        _gameManager.GameOver();
    }

    private IEnumerator FlickerGameOverText()
    {
        while(true)
        {
            _gameOverLabel.enabled = !_gameOverLabel.enabled;
            yield return new WaitForSeconds(_flickerSpeed);
        }
    }
}
