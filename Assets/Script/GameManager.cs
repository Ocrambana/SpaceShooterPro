using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{
    private bool _isGameOver = false;

    void Update()
    {
        if(_isGameOver)
        {
            if(Input.GetKeyDown(KeyCode.R))
            {
                Scene actual = SceneManager.GetActiveScene();
                SceneManager.LoadScene(actual.buildIndex);
            }
            else if(Input.GetKeyDown(KeyCode.Q))
            {
                Application.Quit();
            }
        }
    }

    public void GameOver()
    {
        _isGameOver = true;
    }
}
