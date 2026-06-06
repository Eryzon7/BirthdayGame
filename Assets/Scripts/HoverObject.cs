using UnityEngine;
using TMPro;
using UnityEngine.UI;

public class HoverObject : MonoBehaviour
{
    public GameObject hoverText;
    public GameObject Object;
    public ChatManager chatManager;
    public GameObject Pc;
    public GameObject Tv;

    [SerializeField] private ScrollRect scrollRect;


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
        hoverText.SetActive(false);
        Object.GetComponent<Canvas>().enabled = true;
        Pc.GetComponent<BoxCollider2D>().enabled = false;
        Tv.GetComponent<BoxCollider2D>().enabled = false;
        Canvas.ForceUpdateCanvases();
        scrollRect.verticalNormalizedPosition = 1f;
    }
}
