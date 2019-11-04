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

    void Start()
    {
        UpdateLives(_liveSprites.Count);
        UpdateScore(0);
    }

    public void UpdateScore(int val)
    {
        _scoreText.text = val.ToString();
    }

    public void UpdateLives(int currentLives)
    {
        _livesImage.sprite = _liveSprites[currentLives];
    }
}
