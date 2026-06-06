using Unity.VisualScripting;
using UnityEngine;
using System.Collections;
using TMPro;
using UnityEngine.SceneManagement;

public class CakeLayerManager : MonoBehaviour
{
    [SerializeField] private CakeLayer[] CakeLayers;
    [SerializeField] private float layerDelay = 1f;
    [SerializeField] private Confetti confetti;
    [SerializeField] private TMP_Text victoryText;
    private int ActiveLayer = 0;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        CakeLayers[ActiveLayer].gameObject.SetActive(true);
    }

    private IEnumerator UpdateLayer()
    {
        yield return new WaitForSeconds(layerDelay);

        ActiveLayer++;

        if (ActiveLayer < CakeLayers.Length)
        {
            CakeLayers[ActiveLayer].gameObject.SetActive(true);
        }
        else
        {
            yield return new WaitForSeconds(layerDelay);
            StartCoroutine(WinGame());
        }
    }

    

    public IEnumerator WinGame()
    {
        GameManager.Instance.cakeGameCompleted = true;
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
    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Space))
        {
            CakeLayers[ActiveLayer].CakeDrop();
            StartCoroutine(UpdateLayer());
        }
    }
}
