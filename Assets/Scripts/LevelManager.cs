using System;
using UnityEngine;
using UnityEngine.Audio;
using System.Collections;
using UnityEngine.SceneManagement;

public class Level : MonoBehaviour
{
    int lives = 6;
    public Confetti confetti;
    public Canvas canvas;

    public IEnumerator WinGame()
    {
        GameManager.Instance.paddleGameCompleted = true;
        GameManager.Instance.paddleGameFailed = false;
        confetti.PlayWinEffects();
        canvas.gameObject.SetActive(true);

        // Run on GameManager instead, which persists
        GameManager.Instance.StartCoroutine(LoadAfterDelay(5f));
        yield return null;
    }

    private IEnumerator LoadAfterDelay(float delay)
    {
        yield return new WaitForSecondsRealtime(delay);
        SceneManager.LoadScene("SampleScene");
    }
    void GameOver()
    {
        GameManager.Instance.paddleGameCompleted = true;
        GameManager.Instance.paddleGameFailed = true;
    }

    public void LoseHP()
    {
        lives--;
        if (lives <= 0 )
        {
            GameOver();
        }

    }
}
