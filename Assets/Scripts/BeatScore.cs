using NUnit.Framework.Internal.Commands;
using TMPro;
using UnityEngine;
using System.Collections;
using UnityEngine.SceneManagement;

public class BeatScore : MonoBehaviour
{
    private float score = 0;
    private float combo = 0;
    private float comboMultiplyer = 1;
    [SerializeField] private TMP_Text scoreText;
    [SerializeField] private NoteManager noteManager;
    [SerializeField] private TMP_Text victoryText;
    [SerializeField] private Confetti confetti;

    public void UpdateScore(int add)
    {
        float gainedPoints;
        gainedPoints = ((add * comboMultiplyer) * 100);
        Debug.Log(gainedPoints);
        score = score + gainedPoints;
        scoreText.text = "Score: " + score;
    }

    public void ComboCounter()
    {
        combo++;
        comboMultiplyer = 1 + ((combo - 1) / 10);
    }
    public void ComboReset()
    {
        combo = 0;
        comboMultiplyer = 1;
    }

    private void SongeOver()
    {
        if (score > 5000)
        {
            StartCoroutine(WinGame());
        }
    }

    public IEnumerator WinGame()
    {
        Debug.Log(confetti);
        GameManager.Instance.musicGameCompleted = true;
        GameManager.Instance.musicGameFailed = false;
        confetti.PlayWinEffects();
        victoryText.gameObject.SetActive(true);

        // Run on GameManager instead, which persists
        GameManager.Instance.StartCoroutine(LoadAfterDelay(5f));
        yield return null;
    }

    private IEnumerator LoadAfterDelay(float delay)
    {
        yield return new WaitForSecondsRealtime(delay);
        SceneManager.LoadScene("SampleScene");
    }

    void OnEnable()
    {
        noteManager.songOver += SongeOver;
    }

    void OnDisable()
    {
        noteManager.songOver -= SongeOver;
    }

}
