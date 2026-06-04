using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;
public class HoverHighlight : MonoBehaviour, IPointerEnterHandler, IPointerExitHandler
{
    [SerializeField] private Color highlightColor;
    [SerializeField] private Color normalColor;

    private Image panelImage;

    void Awake()
    {
        panelImage = GetComponent<Image>();
    }
    public void OnPointerEnter(PointerEventData eventData)
    {
        panelImage.color = highlightColor;
    }

    public void OnPointerExit(PointerEventData eventData)
    {
        panelImage.color = normalColor;
    }
}
