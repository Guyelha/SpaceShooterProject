using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UI_Manager : MonoBehaviour
{
    [SerializeField] private Image healthBar;
    [SerializeField] private Text scoreText;
    [SerializeField] private float healthChangeTime = 0.5f;

    private void Awake()
    {
        Health.OnPlayerhealthChanged += UpdateHealthBar;
        GameManager.OnScoreUpdated += UpdateScore;
    }

    private void OnDestroy()
    {
        Health.OnPlayerhealthChanged -= UpdateHealthBar;
        GameManager.OnScoreUpdated -= UpdateScore;

    }

    private void UpdateHealthBar(int totalHealth, int newHealth)
    {
        // healthBar.fillAmount = (float)newHealth / (float)totalHealth;

        StartCoroutine(UpdateHealthCoroutine((float)totalHealth, (float)newHealth));
    }

    private IEnumerator UpdateHealthCoroutine(float totalHealth, float newHealth)
    {
        float preChangeFill = healthBar.fillAmount;
        float destFill = newHealth / totalHealth;
        float elapsedTime = 0;

        while (elapsedTime < healthChangeTime)
        {
            elapsedTime += Time.deltaTime;

            float newFillAmount = Mathf.Lerp(preChangeFill, destFill, elapsedTime / healthChangeTime);
            healthBar.fillAmount = newFillAmount;

            yield return null;
        }
    }


    private void UpdateScore(int newScore)
    {
        scoreText.text = newScore.ToString();
    }
}