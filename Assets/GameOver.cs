using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class GameOver : MonoBehaviour
{
    [SerializeField] private Text scoreText;
    [SerializeField] private Text highScoreText;

    void Start()
    {
        scoreText.text = GameManager.Instance.GetScore().ToString();
        highScoreText.text = GameManager.Instance.GetHighScore().ToString();
    }

    public void RestartGame()
    {
        GameManager.Instance.StartGame();
    }

    public void MainMenu()
    {
        SceneManager.LoadScene("OpeningScreen");
        Time.timeScale = 1f;
    }
}
