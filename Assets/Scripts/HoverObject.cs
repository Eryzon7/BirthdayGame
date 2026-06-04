using UnityEngine;
using TMPro;

public class HoverObject : MonoBehaviour
{
    public GameObject hoverText;
    public GameObject Object;
    public ChatManager chatManager;

    private void OnMouseEnter()
    {
        hoverText.SetActive(true);
    }

    private void OnMouseExit()
    {
        hoverText.SetActive(false);
    }

    
    private void OnMouseDown()
    {
        Object.SetActive(true);
        StartCoroutine(chatManager.ChatSequence());

    }
}
