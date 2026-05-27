using UnityEngine;

public class ClickableObject : MonoBehaviour
{
    public GameObject PC;
        private void OnMouseDown()
        {
            PC.SetActive(true);
        }
}
