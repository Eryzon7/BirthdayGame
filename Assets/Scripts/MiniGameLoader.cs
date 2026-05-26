using UnityEngine;
using UnityEngine.SceneManagement;

public class MiniGameLoader : MonoBehaviour
{
    public void onClick()
    {
        switch (GameManager.Instance.checkMiniGames())
        {
            case 0:
                SceneManager.LoadScene("balloon defend");
                break;

            case 1:
                SceneManager.LoadScene("Music Game");
                break;

            case 2:
                SceneManager.LoadScene("Cake Game");
                break;

            

        }

        
    }
}
