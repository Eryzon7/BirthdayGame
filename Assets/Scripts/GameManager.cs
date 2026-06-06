using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    public static GameManager Instance;

    public List<CakeLayerData> cakeLayers = new List<CakeLayerData>();

    public bool paddleGameCompleted;
    public bool paddleGameFailed;
    public bool musicGameCompleted;
    public bool musicGameFailed;
    public bool cakeGameCompleted;

    private void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
            DontDestroyOnLoad(gameObject);
        }
        else
        {
            Destroy(gameObject);
        }
    }

    public bool AllMinigamesCompleted()
    {
        return paddleGameCompleted &&
               musicGameCompleted &&
               cakeGameCompleted;
    }

    public int checkMiniGames()
    {
        if (!GameManager.Instance.paddleGameCompleted)
        {
            return 0;
        }

        if (GameManager.Instance.paddleGameCompleted && !GameManager.Instance.musicGameCompleted)
        {
            return 1;
        }

        if (GameManager.Instance.musicGameCompleted && !GameManager.Instance.cakeGameCompleted)
        {
            return 2;
        }

        if (GameManager.Instance.AllMinigamesCompleted())
        {
            return 3;
        }

        else return 0;
    }
}
