using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class UIManager : MonoBehaviour
{
    [SerializeField]
    private TextMeshProUGUI _scoreText;

    void Start()
    {
        _scoreText.text = "0";   
    }

    public void UpdateScore(int val)
    {
        _scoreText.text = val.ToString();
    }
}
