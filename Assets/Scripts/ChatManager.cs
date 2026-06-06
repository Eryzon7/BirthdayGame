using System.Collections;
using UnityEngine;
using TMPro;
using UnityEngine.UI;

public class ChatManager : MonoBehaviour
{
    public GameObject messagePrefab;
    [SerializeField] private GameObject decoration;
    [SerializeField] private GameObject TV;
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
        if (GameManager.Instance.paddleGameCompleted)
        {
            decoration.SetActive(true);
        }
        if (GameManager.Instance.musicGameCompleted)
        {
            TV.SetActive(true);

        }

        yield return new WaitForSeconds(3f);
        audioSource.Play();
    }
    public IEnumerator ChatSequence()
    {
        switch(GameManager.Instance.checkMiniGames())
        {
            case 0:
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
                break;
                
            case 1:
                if(GameManager.Instance.paddleGameFailed)
                {
                    yield return SendMessage(new ChatMessage
                    {
                        senderName = SenderName,
                        text = "Unlucky Chou, but don't worry i found some more balloons"
                    });
                }
                else
                {
                    yield return SendMessage(new ChatMessage
                    {
                        senderName = SenderName,
                        text = "Lets go Chou, now we have the decoration for the party"
                    });
                }
                
                yield return new WaitForSeconds(1f);

                yield return SendMessage(new ChatMessage
                {
                    senderName = SenderName,
                    text = "Now for the next thing, what is a party without music"
                });

                yield return new WaitForSeconds(1.5f);

                yield return SendMessage(new ChatMessage
                {
                    senderName = SenderName,
                    text = "Unfortunatly there is a musical lock on the speaker so you have to remove that, good luck"
                });

                yield return new WaitForSeconds(1f);

                Arrow.gameObject.SetActive(true);
                break;
                
            case 2:
                if (GameManager.Instance.musicGameFailed)
                {
                    yield return SendMessage(new ChatMessage
                    {
                        senderName = SenderName,
                        text = "nice try Chou, but don't worry i hacked into the speaker"
                    });
                }
                else
                {
                    yield return SendMessage(new ChatMessage
                    {
                        senderName = SenderName,
                        text = "Lets go Chou, well done opening the lock"
                    });
                }

                yield return new WaitForSeconds(1f);

                yield return SendMessage(new ChatMessage
                {
                    senderName = SenderName,
                    text = "As you can hear we now have music, interact with the TV to change the song"
                });

                yield return new WaitForSeconds(1.5f);

                yield return SendMessage(new ChatMessage
                {
                    senderName = SenderName,
                    text = "I was just informed the cake was delivered to the other building without informing you"
                });

                yield return new WaitForSeconds(1f);

                yield return SendMessage(new ChatMessage
                {
                    senderName = SenderName,
                    text = "So you're gonna have to make a new cake"
                });

                yield return new WaitForSeconds(1f);

                Arrow.gameObject.SetActive(true);
                break;

            case 3:
                break;

        }
        
    }
}