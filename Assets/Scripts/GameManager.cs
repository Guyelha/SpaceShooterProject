using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{
    public static event Action<int> OnScoreUpdated;

    [SerializeField] private int score = 0;
    private int highScore = 0;

    public static GameManager Instance;
    private void Awake()
    {
        if (Instance != null)
            Destroy(gameObject);
        else
            Instance = this;

        DontDestroyOnLoad(gameObject);

        Health.OnEnemyDead += UpdateScore;
    }

    private void OnDestroy()
    {
        Health.OnEnemyDead -= UpdateScore;

    }

    private void UpdateScore()
    {
        score += 1;
        // tell UI manager to change text
        OnScoreUpdated?.Invoke(score);
    }

    public void StartGame()
    {
        score = 0;
        SceneManager.LoadScene(1);
    }

    public void GameOver()
    {
        if (score > highScore)
            highScore = score;

        SceneManager.LoadScene(2);
    }

    public void ExitGame()
    {
        Application.Quit();
        Debug.Log("Quit");
    }

    public int GetScore()
    {
        return score;
    }

    public int GetHighScore()
    {
        return highScore;
    }
}

