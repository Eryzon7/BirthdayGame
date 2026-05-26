using System.Collections;
using UnityEngine;
using TMPro;
using UnityEngine.UI;

public class ChatManager : MonoBehaviour
{
    public GameObject messagePrefab;
    public Transform contentParent;
    public Button Arrow;

    public GameObject typingIndicator;
    public AudioSource audioSource;
    public ScrollRect scrollRect;
    public Sprite Avatar;

    private string SenderName = "Ruben";

    public IEnumerator SendMessage(ChatMessage msg)
    {
        Debug.Log("sendMSG");
        typingIndicator.SetActive(true);
        yield return new WaitForSeconds(Random.Range(0.8f, 1.8f));

        typingIndicator.SetActive(false);

        GameObject obj = Instantiate(messagePrefab);
        obj.transform.SetParent(contentParent, false);

        obj.transform.Find("Name").GetComponent<TMP_Text>().text = msg.senderName;
        obj.transform.Find("Message").GetComponent<TMP_Text>().text = msg.text;
        obj.transform.Find("Avatar").GetComponent<Image>().sprite = Avatar;

        if (msg.sound != null)
            audioSource.PlayOneShot(msg.sound);

        Canvas.ForceUpdateCanvases();
        
        LayoutRebuilder.ForceRebuildLayoutImmediate(contentParent as RectTransform);
        scrollRect.verticalNormalizedPosition = 0f;
        scrollRect.velocity = Vector2.zero;

        yield return StartCoroutine(SlideIn(obj.GetComponent<RectTransform>()));
    }

    IEnumerator SlideIn(RectTransform rect)
    {
        CanvasGroup cg = rect.GetComponent<CanvasGroup>();

        rect.localScale = Vector3.one * 0.95f;
        cg.alpha = 0;

        float t = 0;
        float duration = 0.25f;

        while (t < duration)
        {
            t += Time.deltaTime;
            float n = t / duration;

            rect.localScale = Vector3.Lerp(Vector3.one * 0.95f, Vector3.one, n);
            cg.alpha = n;

            yield return null;
        }

        rect.localScale = Vector3.one;
        cg.alpha = 1;
    }

   
    IEnumerator Start()
    {
        yield return new WaitForSeconds(3f);
        audioSource.Play();
    }
    public IEnumerator ChatSequence()
    {
        yield return SendMessage(new ChatMessage
        {
            senderName = SenderName,
            text = "Hey Chou Happy birthday :)"
        });

        yield return new WaitForSeconds(1f);

        yield return SendMessage(new ChatMessage
        {
            senderName = SenderName,
            text = "Bad news for the party tonight"
        });

        yield return new WaitForSeconds(1.5f);

        yield return SendMessage(new ChatMessage
        {
            senderName = SenderName,
            text = "Primal aspids are attacking the balloons, quick defend them"
        });

        yield return new WaitForSeconds(1f);

        Arrow.gameObject.SetActive(true);
    }
}