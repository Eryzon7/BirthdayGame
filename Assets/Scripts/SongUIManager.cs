using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class SongUIManager : MonoBehaviour
{
    [SerializeField] private SongLibrary library;
    [SerializeField] private Transform contentParent;
    [SerializeField] private GameObject songItemPrefab;

    void Start()
    {
        GenerateList();
    }

    void GenerateList()
    {
        Debug.Log("Library: " + library);
        Debug.Log("ContentParent: " + contentParent);
        Debug.Log("Prefab: " + songItemPrefab);
        Debug.Log("Songs count: " + library?.songs.Count);
        foreach (SongData song in library.songs)
        {
            GameObject item = Instantiate(songItemPrefab, contentParent);

            item.transform.Find("Title").GetComponent<TMP_Text>().text = song.title;
            item.transform.Find("Cover").GetComponent<Image>().sprite = song.cover;
            item.transform.Find("Artist").GetComponent<TMP_Text>().text = song.artist;

            Button button = item.GetComponentInChildren<Button>();
            button.onClick.AddListener(() => PlaySong(song));
        }
    }

    void PlaySong(SongData song)
    {
        Debug.Log("Playing: " + song.title);
        AudioSource.PlayClipAtPoint(song.clip, Vector3.zero);
    }
}